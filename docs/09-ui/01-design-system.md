# Design System

## Purpose

This document defines the visual design tokens and rules for the JLek POS UI. All values are derived from the needs of a restaurant POS environment: high legibility under various lighting, quick visual scanning, and clear state differentiation.

---

## Colors

### Brand Colors

| Token | Usage | Rationale |
|-------|-------|-----------|
| Primary | Primary actions, active navigation, key accents | High contrast against dark POS background |
| Primary Hover | Hover state for primary actions | Slightly lighter for feedback |
| Primary Text | Text on primary backgrounds | White for maximum contrast |

### Neutral Colors

| Token | Usage |
|-------|-------|
| Surface | Page background (dark) |
| Surface Alt | Card, panel, modal background |
| Surface Hover | Hover state for cards/rows |
| Border | Dividers, card borders |
| Text Primary | Primary text |
| Text Secondary | Secondary/label text |
| Text Disabled | Disabled text |

### Status Colors

Each status color maps directly to an aggregate state as defined in the State Machines.

| Token | State Machine States | Usage |
|-------|---------------------|-------|
| Status Draft | Order: Draft | Active orders not yet confirmed |
| Status Confirmed | Order: Confirmed | Orders awaiting fulfillment |
| Status Completed | Order: Completed, Kitchen Ticket: Served | Final states |
| Status Cancelled | Order: Cancelled | Cancelled orders |
| Status Pending | Kitchen Ticket: Pending | Tickets waiting preparation |
| Status Preparing | Kitchen Ticket: Preparing | Active preparation |
| Status Ready | Kitchen Ticket: Ready | Ready to serve |
| Status Available | Dining Table: Available | Table is free |
| Status Occupied | Dining Table: Occupied | Table in use |
| Status Warning | — | Low stock, time warnings |
| Status Error | — | Errors, failures |
| Status Info | — | Informational |

### Mapping: State → Color

| State Machine | State | Color Token |
|---------------|-------|-------------|
| Order | Draft | Status Draft |
| Order | Confirmed | Status Confirmed |
| Order | Completed | Status Completed |
| Order | Cancelled | Status Cancelled |
| Kitchen Ticket | Pending | Status Pending |
| Kitchen Ticket | Preparing | Status Preparing |
| Kitchen Ticket | Ready | Status Ready |
| Kitchen Ticket | Served | Status Completed |
| Dining Table | Available | Status Available |
| Dining Table | Occupied | Status Occupied |

---

## Typography

### Font Family

| Token | Value |
|-------|-------|
| Font Primary | System UI font stack |
| Font Monospace | System monospace stack |

### Font Sizes

| Token | Size | Usage |
|-------|------|-------|
| Text XS | 12px | Labels, timestamps |
| Text SM | 14px | Secondary text, descriptions |
| Text Base | 16px | Body text, table names |
| Text LG | 18px | Cards, section titles |
| Text XL | 20px | Page titles |
| Text 2XL | 24px | Dashboard metrics |
| Text 3XL | 32px | Hero metrics |

### Font Weights

| Token | Weight | Usage |
|-------|--------|-------|
| Weight Normal | 400 | Body text |
| Weight Medium | 500 | Labels, active items |
| Weight Semibold | 600 | Card titles, buttons |
| Weight Bold | 700 | Dashboard metrics, page titles |

---

## Spacing

Based on 4px grid.

| Token | Size | Usage |
|-------|------|-------|
| Space 1 | 4px | Inner padding (badges, icons) |
| Space 2 | 8px | Tight component spacing |
| Space 3 | 12px | Standard component spacing |
| Space 4 | 16px | Card padding, section spacing |
| Space 5 | 20px | Wide component spacing |
| Space 6 | 24px | Section separation, modal padding |
| Space 8 | 32px | Page padding, workspace margins |
| Space 10 | 40px | Major section breaks |
| Space 12 | 48px | Page section separation |

---

## Grid

| Token | Value |
|-------|-------|
| Grid Columns | 12-column grid |
| Gutter | 16px |
| Card Min Width | 200px (tables), 280px (kitchen items) |
| Card Max Width | 400px |
| Workspace Max Width | 1400px (centered) |

### Layout Regions

```text
+-------------------+----------------------------------+
|   Sidebar (fixed) |         Main Content             |
|   240px / 64px    |    flex: 1 (scrollable)          |
|   (expanded/collapsed) |                              |
+-------------------+----------------------------------+
|              Top Bar (fixed, 56px)                     |
+-------------------------------------------------------+
```

---

## Border Radius

| Token | Value | Usage |
|-------|-------|-------|
| Radius SM | 4px | Badges, small icons |
| Radius MD | 8px | Cards, buttons, inputs |
| Radius LG | 12px | Modals, dialogs |
| Radius XL | 16px | Large panels |
| Radius Full | 9999px | Pills, avatars |

---

## Elevation

| Level | Usage |
|-------|-------|
| 0 | Surface-level elements (cards, panels) |
| 1 | Elevated elements (hover states, dropdowns) |
| 2 | Modals, dialogs |
| 3 | Notifications, toasts |

---

## Shadows

| Token | Value |
|-------|-------|
| Shadow SM | Small cards, subtle elevation |
| Shadow MD | Modals, dialogs |
| Shadow LG | Notifications, toasts |

---

## Icons

| Rule | Description |
|------|-------------|
| Icon Library | Use a consistent icon set (phosphor, material, or custom) |
| Icon Size | 20px for inline, 24px for standalone, 32px for status indicators |
| Icon Color | Inherit from text color or status color |
| State Icons | Each business state has an associated icon for quick scanning |

### State Icons

| State | Icon Concept |
|-------|-------------|
| Draft | File/edit |
| Confirmed | Checkmark circle |
| Completed | Double checkmark |
| Cancelled | X circle |
| Pending | Clock |
| Preparing | Cooking/fire |
| Ready | Bell |
| Served | Hand/finished |
| Available | Circle/open |
| Occupied | User/customer |

---

## Motion

| Transition | Duration | Easing | Usage |
|------------|----------|--------|-------|
| Hover | 150ms | Ease | Button, card hover |
| Visibility | 200ms | Ease | Show/hide elements |
| Navigation | 250ms | Ease In Out | Page transitions |
| Modal | 300ms | Ease Out | Modal enter/exit |
| Notification | 300ms | Ease Out | Toast enter, 300ms exit |

---

## Status Colors

| Token | Visual Indication |
|-------|-------------------|
| Status Draft | Neutral / gray |
| Status Confirmed / Pending | Blue / information |
| Status Preparing / Occupied | Orange / active |
| Status Ready | Violet / attention needed |
| Status Completed / Served / Available | Green / success |
| Status Cancelled | Red / failure |

---

## Breakpoints

| Breakpoint | Width | Target |
|------------|-------|--------|
| XS | < 480px | Mobile phone |
| SM | ≥ 480px | Large phone |
| MD | ≥ 768px | Tablet |
| LG | ≥ 1024px | Small desktop |
| XL | ≥ 1280px | Desktop |
| 2XL | ≥ 1536px | Wide desktop |

---

## Responsive Rules

| Device | Sidebar | Top Bar | Card Layout |
|--------|---------|---------|-------------|
| Desktop (≥1024px) | Expanded (240px) | Full | Multi-column grid |
| Tablet (≥768px) | Collapsed (64px) | Compact | 2-column grid |
| Mobile (<768px) | Hidden (drawer) | Minimal | Single column |

### Workspace-Specific Rules

| Workspace | Priority | Responsive Behavior |
|-----------|----------|---------------------|
| Cashier | High | Grid collapses to 2 columns on tablet, 1 on mobile |
| Kitchen | High | 4-column queue → 2 columns on tablet → scroll on mobile |
| Dashboard | Medium | Metric cards wrap, tables scroll horizontally |
| Reports | Medium | Tables scroll horizontally on small screens |
| Settings | Low | Single column, full width |