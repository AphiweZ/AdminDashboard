# Pulse — SaaS Admin Dashboard (C# / Blazor)

A full admin dashboard built entirely in C#, using **Blazor Web App** (.NET 8,
interactive server render mode) instead of React. It covers the same ground
as the original shadcn/React brief, translated to the .NET/Visual Studio
world:

| Original ask                  | What this uses instead                          |
|-------------------------------|--------------------------------------------------|
| shadcn/ui components          | Custom Razor components styled with Tailwind CSS |
| Recharts                      | Chart.js, wired up via JS interop                |
| React state/routing           | Blazor Server components + built-in router       |
| Sidebar, stat cards, tables   | Same structure, built as `.razor` components      |

## Opening it in Visual Studio

1. Double-click **`AdminDashboard.sln`** — it opens the project directly.
2. Press **F5** (or the green ▶ Run button). Visual Studio restores NuGet
   packages and launches the app in your browser automatically.
3. No Node.js, npm, or extra tooling required — it's a self-contained
   ASP.NET Core project.

Requires the **.NET 8 SDK** and the **ASP.NET and web development** workload
in Visual Studio 2022 (17.8+). If Visual Studio prompts to install the SDK,
let it — that's the only external dependency.

## What's included

- **Sidebar navigation** — Dashboard, Users, Projects, Settings, with a
  collapse toggle (`Components/Layout/MainLayout.razor`, `NavMenu.razor`).
- **Dashboard page** — 4 stat cards (Total Users, Active Projects, Revenue,
  Growth) plus a line chart (revenue over time) and a bar chart (signups by
  month), rendered with Chart.js.
- **Users page** — searchable, sortable (click any column header),
  paginated data table with adjustable page size.
- **Settings page** — grouped form fields (workspace info, notification
  toggles) with a save action.
- **Role-based access** — a "Viewing as" switcher in the topbar simulates
  Admin / Editor / Viewer. Viewers lose access to Settings (nav item is
  locked) and can't manage users (no invite/remove buttons on the Users
  page). This stands in for a real auth system — see below.
- **Dark mode** — toggle in the topbar; also respects the OS preference on
  first load. Implemented with Tailwind's `class` dark mode strategy plus
  CSS variables in `wwwroot/css/app.css`.

## Project structure

```
AdminDashboard/
  Components/
    Layout/         MainLayout, NavMenu, NavItem
    Pages/           Dashboard, Users, Projects, Settings
    Shared/          StatCard, LineChart, BarChart, Badge, SortHeader
    App.razor        Root HTML document (Tailwind + Chart.js script tags)
    Routes.razor     Router configuration
  Models/            AppUser, ProjectItem, ChartPoint
  Services/
    MockDataService.cs     In-memory data (stand-in for a database)
    ThemeService.cs        Dark mode state
    CurrentUserService.cs  Simulated logged-in user + role
  wwwroot/
    css/app.css      Theme tokens (light/dark CSS variables)
    js/charts.js     Chart.js render/destroy + theme helpers
  Program.cs
```

## Extending this to a real app

This is a fully working demo with realistic seed data, but two things are
intentionally simplified so you can wire in your own infrastructure:

1. **Data layer** — `MockDataService` holds everything in memory and resets
   on restart. Replace it with an `IUserRepository`/`DbContext` (EF Core +
   SQL Server, PostgreSQL, SQLite, etc.) behind the same method signatures
   and the pages won't need to change.
2. **Authentication & roles** — `CurrentUserService` is a plain C# class you
   switch manually in the UI. For real auth, swap in **ASP.NET Core
   Identity**, **Entra ID**, or **Auth0**, and read the role from
   `ClaimsPrincipal` claims instead — the `CanManageUsers` /
   `CanEditSettings` properties are already isolated so only that one file
   needs to change.
3. **Styling** — Tailwind is loaded from the Play CDN for zero-build
   simplicity. For production, install Tailwind via npm and compile a
   static `app.css` instead (the CDN build warns about this in the browser
   console, by design).

## Notes

- All data (users, projects, revenue, signups) is seeded with a fixed random
  seed, so numbers are consistent across restarts but not "real."
- Charts, tables, and forms all use plain C#/Razor — no JavaScript
  frameworks, npm, or bundler involved beyond the two `<script>` CDN tags
  for Tailwind and Chart.js.
