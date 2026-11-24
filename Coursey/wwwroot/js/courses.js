
document.addEventListener("DOMContentLoaded", function () {

    const searchInput = document.getElementById("search");
    const subjectInput = document.getElementById("subject");
    const difficultyInput = document.getElementById("difficulty");
    const sortOrder = document.getElementById("sortOrder");
    const coursesTable = document.getElementById("coursesTable");
    const clearFilters = document.getElementById("clearFilters");

    const url = coursesTable.dataset.url;

    const detailsUrl = coursesTable.dataset.detailsUrl;
    const editUrl = coursesTable.dataset.editUrl;
    const deleteUrl = coursesTable.dataset.deleteUrl;

    let debounceTimer;
    let currentController = null;

    function formatDate(date) {

        if (!date) {
            return "";
        }

        const parts = date.split("-");

        if (parts.length !== 3) {
            return date;
        }

        return `${parts[2]}.${parts[1]}.${parts[0]}`;
    }

    function createActionLink(text, url, className) {

        const link = document.createElement("a");

        link.href = url.replace("__id__", encodeURIComponent(arguments[3]));
        link.textContent = text;
        link.className = className;

        return link;
    }

    function renderCourses(courses) {

        coursesTable.innerHTML = "";

        if (!courses || courses.length === 0) {

            const row = document.createElement("tr");
            const cell = document.createElement("td");

            cell.colSpan = 7;
            cell.className = "text-center";
            cell.textContent = "No courses were found.";

            row.appendChild(cell);
            coursesTable.appendChild(row);

            return;
        }

        courses.forEach(course => {

            const id = course.id ?? course.Id;
            const name = course.name ?? course.Name;
            const subject = course.subject ?? course.Subject;
            const startDate = course.startDate ?? course.StartDate;
            const endDate = course.endDate ?? course.EndDate;
            const difficulty = course.difficulty ?? course.Difficulty;
            const teacherName = course.teacherName ?? course.TeacherName;

            const row = document.createElement("tr");

            const nameCell = document.createElement("td");
            nameCell.textContent = name ?? "";

            const subjectCell = document.createElement("td");
            subjectCell.textContent = subject ?? "";

            const startDateCell = document.createElement("td");
            startDateCell.textContent = formatDate(startDate);

            const endDateCell = document.createElement("td");
            endDateCell.textContent = formatDate(endDate);

            const difficultyCell = document.createElement("td");
            difficultyCell.textContent = difficulty || "Not specified";

            const teacherCell = document.createElement("td");
            teacherCell.textContent = teacherName ?? "";

            const actionsCell = document.createElement("td");
            actionsCell.className = "text-center";

            const buttonGroup = document.createElement("div");
            buttonGroup.className = "btn-group";
            buttonGroup.setAttribute("role", "group");

            const detailsLink = createActionLink(
                "Details",
                detailsUrl,
                "btn btn-sm btn-outline-info",
                id
            );

            const editLink = createActionLink(
                "Edit",
                editUrl,
                "btn btn-sm btn-outline-warning",
                id
            );

            const deleteLink = createActionLink(
                "Delete",
                deleteUrl,
                "btn btn-sm btn-outline-danger",
                id
            );

            buttonGroup.append(
                detailsLink,
                editLink,
                deleteLink
            );

            actionsCell.appendChild(buttonGroup);

            row.append(
                nameCell,
                subjectCell,
                startDateCell,
                endDateCell,
                difficultyCell,
                teacherCell,
                actionsCell
            );

            coursesTable.appendChild(row);
        });
    }

    async function filterCourses() {

        clearTimeout(debounceTimer);

        if (currentController) {
            currentController.abort();
        }

        currentController = new AbortController();

        const params = new URLSearchParams({
            search: searchInput.value,
            subject: subjectInput.value,
            difficulty: difficultyInput.value,
            sortOrder: sortOrder.value
        });

        try {

            const response = await fetch(
                `${url}?${params.toString()}`,
                {
                    signal: currentController.signal
                }
            );

            if (!response.ok) {
                throw new Error(`HTTP Error: ${response.status}`);
            }

            const courses = await response.json();

            renderCourses(courses);

        } catch (error) {

            if (error.name !== "AbortError") {
                console.error("Error filtering courses:", error);
            }
        }
    }

    function debounceFilter() {

        clearTimeout(debounceTimer);

        debounceTimer = setTimeout(() => {
            filterCourses();
        }, 300);
    }

    searchInput.addEventListener("input", debounceFilter);
    subjectInput.addEventListener("input", debounceFilter);
    difficultyInput.addEventListener("input", debounceFilter);

    sortOrder.addEventListener("change", filterCourses);

    clearFilters.addEventListener("click", function () {

        searchInput.value = "";
        subjectInput.value = "";
        difficultyInput.value = "";
        sortOrder.value = "name_asc";

        filterCourses();
    });

});
