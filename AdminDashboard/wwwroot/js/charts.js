window.pulseCharts = (function () {
    const instances = {};

    function baseOptions() {
        return {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }
            },
            scales: {
                x: { grid: { display: false } },
                y: { grid: { color: "rgba(148, 163, 184, 0.15)" }, beginAtZero: true }
            }
        };
    }

    function renderLine(canvasId, labels, values, color) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;
        if (instances[canvasId]) instances[canvasId].destroy();

        instances[canvasId] = new Chart(ctx, {
            type: "line",
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    borderColor: color,
                    backgroundColor: color + "33",
                    fill: true,
                    tension: 0.35,
                    pointRadius: 3,
                    pointBackgroundColor: color
                }]
            },
            options: baseOptions()
        });
    }

    function renderBar(canvasId, labels, values, color) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;
        if (instances[canvasId]) instances[canvasId].destroy();

        instances[canvasId] = new Chart(ctx, {
            type: "bar",
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: color,
                    borderRadius: 6,
                    maxBarThickness: 36
                }]
            },
            options: baseOptions()
        });
    }

    function destroy(canvasId) {
        if (instances[canvasId]) {
            instances[canvasId].destroy();
            delete instances[canvasId];
        }
    }

    return { renderLine, renderBar, destroy };
})();

window.pulseTheme = (function () {
    function setDarkMode(isDark) {
        document.documentElement.classList.toggle("dark", isDark);
    }

    function prefersDark() {
        return !!(window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches);
    }

    // Apply the OS preference immediately on first load (before Blazor's
    // circuit even connects) so there's no light-mode flash for dark-mode users.
    if (prefersDark()) {
        document.documentElement.classList.add("dark");
    }

    return { setDarkMode, prefersDark };
})();
