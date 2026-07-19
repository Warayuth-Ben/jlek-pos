# Workspace Platform

## Every Workspace Shares the Same Skeleton

```text
Application ──────────────────────────────────────────────
       │
       ▼
Workspace (platform instance)
       │
       ├── Header
       │     ├── Logo / App Name
       │     ├── Workspace Title
       │     ├── Global Actions (notification, profile)
       │     └── Workspace Switcher (sidebar toggle)
       │
       ├── Context Bar
       │     ├── Current selection (e.g., Table 8)
       │     ├── Context status
       │     ├── Context actions
       │     └── Breadcrumb (if applicable)
       │
       ├── Toolbar
       │     ├── Primary action(s)
       │     ├── Secondary actions
       │     ├── Search / Filter
       │     └── Refresh / View toggle
       │
       ├── Primary Content
       │     ├── Main workspace component
       │     ├── Loading / Empty / Error states
       │     └── State-driven rendering
       │
       ├── Secondary Content
       │     ├── Detail panel (optional)
       │     ├── Context-dependent
       │     └── Slides in / out
       │
       ├── Action Area
       │     ├── Primary workflow actions
       │     ├── Context-sensitive
       │     └── Fixed or inline
       │
       ├── Activity Feed
       │     ├── Recent events (optional)
       │     └── Context-dependent
       │
       └── Status Bar
             ├── Aggregate counts
             ├── Connection status
             └── System status
```

---

## Section Responsibilities

### Header

The Header identifies the current workspace and provides global navigation.

| Responsibility | Description |
|---------------|-------------|
| Workspace identification | Display workspace name so user knows where they are |
| Global navigation | Sidebar toggle, workspace switcher |
| System-level actions | Notifications, profile, logout |

**Rules:**
- Header is always visible in every workspace
- Header does not change based on workspace content
- Header height is fixed (56px)

### Context Bar

The Context Bar displays the current selection context.

| Responsibility | Description |
|---------------|-------------|
| Show what is selected | "Table 8", "Order #1024", "Kitchen Ticket #5" |
| Show context status | "Occupied", "Draft", "Pending" |
| Provide context actions | "Close Table", "Print Receipt" |
| Show breadcrumb | Optional navigation path |

**Rules:**
- Context Bar appears only when a context is active
- Context Bar is always visible when context exists
- Context persists across navigation (see context-platform)
- Context Bar height is 40px

### Toolbar

The Toolbar provides actions and controls for the current primary content.

| Responsibility | Description |
|---------------|-------------|
| Primary action | The most important action for the current view |
| Secondary actions | Additional relevant actions |
| Search / Filter | Narrow down displayed content |
| Refresh | Force data refresh |
| View toggle | Switch between views (grid/list/columns) |

**Rules:**
- Primary action is visually prominent (button)
- Secondary actions are visually subdued (icon buttons)
- Search is debounced (300ms)
- Toolbar is always visible
- Toolbar height is 48px

### Primary Content

The Primary Content area displays the main workspace component.

| Responsibility | Description |
|---------------|-------------|
| Main business data | Tables, orders, tickets, reports, settings |
| State rendering | Loading → Ready → Empty / Error |
| State machine display | Aggregate states rendered as components |

**Rules:**
- Primary content fills available space (flex: 1)
- Primary content is scrollable when content exceeds viewport
- Primary content always has loading, empty, and error states
- Primary content never displays raw API data (always through components)

### Secondary Content

The Secondary Content area displays context-dependent detail.

| Responsibility | Description |
|---------------|-------------|
| Detail view | When a primary item is selected, show details |
| Side panel | Fixed-width panel on the right (400px) or bottom sheet on mobile |

**Rules:**
- Secondary Content appears only when a selection exists
- Secondary Content is dismissable
- Secondary Content does not overlap primary content on desktop
- Secondary Content overlays primary content on mobile

### Action Area

The Action Area contains the primary workflow actions for the current context.

| Responsibility | Description |
|---------------|-------------|
| Primary workflow actions | Confirm, Pay, Start, Complete, Serve |
| Context-sensitive | Actions depend on the current selection and its state |

**Rules:**
- Action Area is always at the bottom of the content (or side panel)
- Actions are state-dependent (enabled/disabled based on State Machine)
- Only one primary action per context
- Destructive actions are visually distinct

### Activity Feed

The Activity Feed shows recent events relevant to the current workspace.

| Responsibility | Description |
|---------------|-------------|
| Recent events | Timestamped activity entries |
| Context-dependent | Events filtered by current workspace relevance |

**Rules:**
- Activity Feed is optional (shown only when useful)
- Activity Feed is collapsible
- Activity Feed is read-only

### Status Bar

The Status Bar provides aggregate information and system status.

| Responsibility | Description |
|---------------|-------------|
| Aggregate counts | "4 Occupied", "5 Active Orders", "3 Pending" |
| Connection status | Online / Offline |
| Last refresh time | "Updated 15s ago" |

**Rules:**
- Status Bar is always visible
- Status Bar is fixed at bottom
- Status Bar height is 32px
- Connection status is directly visible

---

## Platform Sections Per Workspace

| Section | Cashier | Kitchen | Dashboard | Reports | Settings |
|---------|---------|---------|-----------|---------|----------|
| Header | ✅ | ✅ | ✅ | ✅ | ✅ |
| Context Bar | ✅ (table) | ✅ (ticket) | ❌ | ❌ | ❌ |
| Toolbar | ✅ | ✅ | ✅ | ✅ | ✅ |
| Primary Content | Table Grid | Kitchen Queue | Metrics | Report Tables | Settings Form |
| Secondary Content | Order Panel (side) | Ticket Detail (modal) | ❌ | ❌ | ❌ |
| Action Area | Confirm/Pay/Cancel | Start/Complete/Serve | ❌ | ❌ | ❌ |
| Activity Feed | ✅ (recent orders) | ✅ (recent served) | ❌ | ❌ | ❌ |
| Status Bar | ✅ | ✅ | ✅ | ✅ | ✅ |