# Interaction Platform

## Purpose

Define how users interact with the UI Platform. Interaction covers input methods (keyboard, mouse, touch), accessibility, and navigation rules.

---

## Keyboard First

The UI Platform is designed for keyboard-first interaction. Restaurant staff work in fast-paced environments where reaching for a mouse slows them down.

### Why Keyboard First

- Cashiers enter orders faster with keyboard shortcuts
- Kitchen staff update ticket status with single keystrokes
- Managers navigate reports without lifting hands from keyboard
- Accessibility requirements are met by default

### Global Shortcuts

| Shortcut | Action | Workspace |
|----------|--------|-----------|
| F1 | Open Help | All |
| F2 | Focus search bar | All (with search) |
| Ctrl+1 | Navigate to Home | All |
| Ctrl+2 | Navigate to Cashier | All |
| Ctrl+3 | Navigate to Kitchen | All |
| Ctrl+4 | Navigate to Dashboard | All |
| Ctrl+5 | Navigate to Reports | All |
| Ctrl+6 | Navigate to Settings | All |
| Escape | Deselect / Close modal | All |
| Ctrl+R | Refresh current workspace | All |

### Workspace Shortcuts

See each workspace document for workspace-specific shortcuts.

---

## Mouse

Mouse interaction follows standard web patterns.

| Action | Gesture | Response |
|--------|---------|----------|
| Select | Click | Entity selected |
| Open | Double-click | Opens entity (table, order, ticket) |
| Context | Right-click | Context menu (future) |
| Hover | Hover | Tooltip, highlight |
| Drag | Click + drag | Reorder, move (future) |

**Rules:**
- Click is the primary selection gesture
- Double-click is for quick-open (not required for primary flow)
- Right-click is reserved for future context menus
- Hover provides additional information without blocking

---

## Touch

Touch interaction follows mobile/tablet patterns.

| Gesture | Action | Context |
|---------|--------|---------|
| Tap | Select, activate | All |
| Tap & Hold | Context menu | Tables, orders, tickets |
| Swipe Left | Reveal action (delete) | Order items |
| Swipe Right | Mark complete | Kitchen tickets |
| Pinch | Zoom | Reports |

**Rules:**
- Touch targets are minimum 44×44px
- Swipe gestures have visual feedback (item slides)
- Tap & Hold time is 500ms before context menu appears
- Touch and mouse coexist without conflict

---

## Command Palette

A Command Palette is a searchable list of all available actions (future).

| Feature | Description |
|---------|-------------|
| Trigger | Ctrl+K or F1 |
| Scope | Current workspace actions + global actions |
| Search | Fuzzy search by action name |
| Selection | Arrow keys + Enter to execute |
| Dismiss | Escape |

---

## Quick Actions

Quick Actions are one-step operations accessible from the toolbar or keyboard.

| Workspace | Quick Actions |
|-----------|---------------|
| Cashier | Open Table, Confirm Order, Process Payment |
| Kitchen | Start Preparation, Complete, Serve |
| Dashboard | Refresh Metrics |
| Reports | Change Date Range, Export (future) |

---

## Accessibility

### Keyboard Navigation

| Element | Tab Order | Behavior |
|---------|-----------|----------|
| Header | 1-10 | Logo, workspace switcher, notifications, profile |
| Toolbar | 11-20 | Primary action, search, filter, refresh |
| Content | 21-50 | Cards, items, list (arrow keys inside content) |
| Action area | 51-60 | Primary action, secondary actions |
| Status bar | 61-70 | Not tabbable (informational) |

### Focus Order

Tab order follows reading order: top to bottom, left to right.

```text
Header → Toolbar → Content (cards) → Action area → Status bar
                      │
                      └── Modal opens → Modal content → Close modal
                                             │
                                             └── Back to triggering element
```

### Focus Indicators

- Every focusable element has a visible focus ring
- Focus ring is 2px solid with 2px offset
- Focus ring color is the primary action color
- Focus ring is always visible (not :focus-visible only)

### Screen Reader

| Element | ARIA |
|---------|------|
| Workspace | role="application", aria-label="{workspace name}" |
| Card | role="button", tabindex="0", aria-label="{entity name}, {status}" |
| Status Badge | aria-label="{status}" |
| Toast | role="status", aria-live="polite" |
| Modal | role="dialog", aria-modal="true", aria-label="..." |
| Table Grid | role="grid", aria-label="Dining tables" |
| Loading | aria-busy="true" |
| Empty | aria-label="No items available" |
| Error | role="alert" |

### Color Contrast

All interactive elements meet WCAG 2.1 AA contrast ratios:
- Normal text: 4.5:1 minimum
- Large text (18px+): 3:1 minimum
- UI components: 3:1 minimum
- Focus indicator: 3:1 minimum against adjacent colors

---

## Focus Rules

| Rule | Description |
|------|-------------|
| Focus is visible | Every focusable element has a visible focus indicator |
| Focus order is logical | Tab order follows visual reading order |
| Focus is trapped in modals | Tab cycles within modal, cannot tab out |
| Focus returns on close | After modal/panel closes, focus returns to triggering element |
| Skip link provided | "Skip to content" link at top of page |
| No keyboard traps | All focusable elements can be reached and left with keyboard |

---

## Navigation Rules

| Rule | Description |
|------|-------------|
| Sidebar navigation is primary | All workspace switches go through sidebar |
| Keyboard navigation is primary | Ctrl+1 through Ctrl+6 switches workspaces |
| Back navigation restores state | Previous workspace context is preserved |
| Navigation does not lose data | Unsaved changes prompt before navigating away |
| External links open in new window | All external documentation links |