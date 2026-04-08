# ✈️ Roamy — Travel Planner

A travel planning web application built with Blazor WebAssembly and C#. Roamy lets you organize your trips with a day-view calendar, activity scheduling, and a shortlist for saving ideas before committing to a time.

> **Status:** MVP complete — actively in development. See the [roadmap](#roadmap) for upcoming features.

---

## Features

### Trip Creation
- Create a trip by entering a destination and date range
- Trip dashboard shows name, location, date range, and days planned

### Day View Calendar
- 15-minute granularity time grid spanning a full 24-hour day
- Navigate between trip days with previous/next controls
- Activities are positioned and sized on the grid based on their start time and duration
- Color-coded activity cards by category

### Activity Management
- Add activities with name, category, address, date, start/end time, and notes
- Edit any activity by clicking its card on the calendar
- Delete activities from the edit modal
- End time automatically defaults to 1 hour after start time
- Activities without a date or start time are saved to the shortlist

### Activity Categories
Each category has its own color theme across the app:
| Category | Color |
|---|---|
| 🟢 Sightseeing | Sage green |
| 🟡 Food & Drink | Amber |
| 🔵 Transport | Sky blue |
| 🟣 Lodging | Lavender |
| 🔴 Event | Red |
| ⚪ Other | Neutral |

### Shortlist Panel
- Save activity ideas without committing to a date or time
- Click any shortlisted activity to schedule it
- Scheduled activities automatically move from the shortlist to the calendar

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | Blazor WebAssembly (.NET) |
| Language | C# |
| Backend | ASP.NET Core Web API (.NET 10) |
| Database | PostgreSQL via Supabase |
| ORM | EF Core 10 + Npgsql |
| Styling | CSS Grid, CSS Custom Properties, Scoped CSS |
| State Management | Singleton service (`TripManager`) with `Action` event callbacks |
| Routing | Blazor built-in routing (`@page`) |
| Component Communication | Blazor `EventCallback<T>` |
| Icons | Heroicons (SVG) |
| Fonts | Google Fonts |

---

## Architecture

```
Roamy/                              ← solution root (.sln lives here)
├── Roamy.Server/
│   ├── Controllers/
│   │   └── WeatherForecastController.cs
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── DesignTimeDbContextFactory.cs
│   ├── Migrations/
│   ├── Repositories/
│   │   ├── BaseRepository.cs
│   │   ├── ITripRepository.cs / TripRepository.cs
│   │   ├── IActivityRepository.cs / ActivityRepository.cs
│   │   ├── IDayRepository.cs / DayRepository.cs
│   │   ├── ITripLocationRepository.cs / TripLocationRepository.cs
│   │   └── IActivityLocationRepository.cs / ActivityLocationRepository.cs
│   ├── appsettings.json
│   └── Program.cs
├── Roamy.Shared/
│   └── Models/
│       ├── Trip.cs
│       ├── Day.cs
│       ├── Activity.cs
│       ├── TripLocation.cs
│       └── ActivityLocation.cs
├── Pages/
│   ├── Home.razor + Home.razor.css
│   ├── Planner.razor + Planner.razor.css
│   └── ActivityModal.razor + ActivityModal.razor.css
├── Layout/
│   ├── MainLayout.razor
│   └── LandingLayout.razor
├── Services/
│   └── TripManager.cs
└── wwwroot/
    └── css/
        └── app.css
```

### Key Design Decisions

**Singleton TripManager** — All data lives in a single `TripManager` service registered as a singleton in `Program.cs`. Components inject it and subscribe to `OnChange` events to re-render when data changes. This avoids prop drilling and keeps state management simple for an MVP.

**Separation of Date and StartTime** — An activity can exist in three states:
- `Date == null` → Shortlist (no date, no time)
- `Date != null, StartTime == null` → Unscheduled (has a date, no time slot yet)
- `Date != null, StartTime != null` → Scheduled (appears on the time grid)

**CSS Grid Calendar** — The time grid uses CSS Grid with 96 rows (15 minutes each) and 2 columns (labels + events). Activity cards are absolutely positioned over the grid using percentage-based `top` and `height` values computed from start time and duration.

**EventCallback Pattern** — `ActivityModal` communicates back to `Planner` using three `EventCallback` parameters: `OnSave`, `OnDelete`, and `OnCancel`. This keeps the modal reusable and decoupled from the parent page.

---

## Roadmap

### Coming in V2
- [x] PostgreSQL database via Supabase + Entity Framework Core
- [x] ASP.NET Core backend API
- [ ] Interactive map panel with Leaflet.js
- [ ] Activity pins on map colored by category
- [ ] Google Places autocomplete for activity addresses
- [ ] Overlapping activity cards (side by side like Google Calendar)
- [ ] Current time indicator line on the calendar
- [ ] Toast notifications with undo for delete

---

## Development

### Branching Strategy
- **Default branch:** `dev` — all feature work branches off here
- **Protected branch:** `main` — requires a PR to merge, never commit directly
- **Branch naming:** `feature/issue-N-short-description`
- **PR descriptions:** always include `Closes #N` to auto-close the related issue on merge

### Setup Notes
- `appsettings.Development.json` is gitignored — this file contains the Supabase connection string and should never be committed. If you clone this repo, you will need to create this file locally with your own connection string.
- `Update-Database` does not work with Supabase + Npgsql 10. Always use `Script-Migration` to generate SQL and run it manually in the Supabase SQL Editor instead.

---

## Author

**Lucas Cano** — [@lucascano98](https://github.com/lucascano98)

---

## License

This project is open source and available under the [MIT License](LICENSE).
