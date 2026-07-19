# JLek POS — CSS Selector Map

> Selector → Target File
>
> ใช้สำหรับค้นหาว่า CSS Selector ใดอยู่ในไฟล์ไหน
> อัปเดตเมื่อมีการย้าย CSS ใน Sprint B–E

---

## Foundation

| Selector | File |
|----------|------|
| `:root` (CSS custom properties) | `css/foundation/variables.css` |
| `*` | `css/foundation/reset.css` |
| `html, body, #app` | `css/foundation/reset.css` |
| `body` | `css/foundation/reset.css` |
| `button, input, select` | `css/foundation/reset.css` |
| `h1` | `css/foundation/typography.css` |
| `h2` | `css/foundation/typography.css` |
| `.pos-kicker`, `.eyebrow` | `css/foundation/typography.css` |
| `.hero-copy` | `css/foundation/typography.css` |
| `.pos-live-dot` | `css/foundation/utilities.css` |

---

## Layout

| Selector | File |
|----------|------|
| `.pos-shell` | `css/layout/app-shell.css` |
| `.pos-rail` | `css/layout/app-shell.css` |
| `.pos-main` | `css/layout/app-shell.css` |
| `.pos-content` | `css/layout/app-shell.css` |
| `.workspace-page` | `css/layout/app-shell.css` |
| `.pos-topbar` | `css/layout/header.css` |
| `.pos-title` | `css/layout/header.css` |
| `.pos-topbar-actions` | `css/layout/header.css` |
| `.pos-brand` | `css/layout/sidebar.css` |
| `.brand-mark` | `css/layout/sidebar.css` |
| `.brand-copy` | `css/layout/sidebar.css` |
| `.pos-nav` | `css/layout/navigation.css` |
| `.pos-nav-group` | `css/layout/navigation.css` |
| `.pos-nav-link` | `css/layout/navigation.css` |
| `.nav-icon` | `css/layout/navigation.css` |
| `.nav-badge` | `css/layout/navigation.css` |
| `.nav-badge-hot` | `css/layout/navigation.css` |

---

## Components

| Selector | File |
|----------|------|
| `button` | `css/components/button.css` |
| `.icon-button` | `css/components/button.css` |
| `.profile-button` | `css/components/button.css` |
| `.primary-action` | `css/components/button.css` |
| `.secondary-action` | `css/components/button.css` |
| `.mini-button` | `css/components/button.css` |
| `.ticket-action` | `css/components/button.css` |
| `button:disabled` | `css/components/button.css` |
| `.surface-panel` | `css/components/card.css` |
| `.metric-tile` | `css/components/card.css` |
| `.launch-card` | `css/components/card.css` |
| `.ticket-card` | `css/components/card.css` |
| `.state-pill` | `css/components/badge.css` |
| `.segmented-control` | `css/components/badge.css` |
| `.segmented-control .active` | `css/components/badge.css` |
| `.date-input` | `css/components/form.css` |
| `.settings-form` | `css/components/form.css` |
| `.settings-form input` | `css/components/form.css` |
| `.settings-form select` | `css/components/form.css` |
| `.settings-form label` | `css/components/form.css` |
| `.toggle-row` | `css/components/form.css` |
| `.range-readout` | `css/components/form.css` |
| `.table-grid-redesign` | `css/components/table.css` |
| `.table-tile` | `css/components/table.css` |
| `.toast-card` | `css/components/toast.css` |

---

## Features

### Cashier

| Selector | File |
|----------|------|
| `.cashier-layout` | `css/features/cashier/cashier.css` |
| `.table-map` | `css/features/cashier/cashier.css` |
| `.order-builder` | `css/features/cashier/cashier.css` |
| `.menu-chips` | `css/features/cashier/product-grid.css` |
| `.order-lines` | `css/features/cashier/order-summary.css` |
| `.order-line` | `css/features/cashier/order-summary.css` |
| `.bill-dock` | `css/features/cashier/order-summary.css` |
| `.empty-state` | `css/features/cashier/order-summary.css` |
| `.payment-panel` | `css/features/cashier/payment-panel.css` |

### Kitchen

| Selector | File |
|----------|------|
| `.kanban-grid` | `css/features/kitchen/kitchen.css` |
| `.queue-column` | `css/features/kitchen/kitchen.css` |
| `.ticket-top` | `css/features/kitchen/kitchen-ticket.css` |
| `.ticket-card ul` | `css/features/kitchen/kitchen-ticket.css` |
| `.ticket-card.preparing` | `css/features/kitchen/kitchen-ticket.css` |
| `.ticket-card.ready` | `css/features/kitchen/kitchen-ticket.css` |

### Dashboard

| Selector | File |
|----------|------|
| `.metric-strip` | `css/features/dashboard/dashboard.css` |
| `.dashboard-panels` | `css/features/dashboard/dashboard.css` |
| `.workspace-heading` | `css/features/dashboard/dashboard.css` |
| `.workspace-hero` | `css/features/dashboard/dashboard.css` |

### Reports

| Selector | File |
|----------|------|
| `.report-canvas` | `css/features/reports/reports.css` |
| `.report-tabs` | `css/features/reports/reports.css` |

### Settings

| Selector | File |
|----------|------|
| `.settings-grid` | `css/features/settings/settings.css` |

---

## Responsive / Media Queries

| Breakpoint | Contained Selectors | File |
|------------|-------------------|------|
| `@media (min-width: 1180px)` | `.pos-shell`, `.pos-rail`, `.pos-brand`, `.brand-copy`, `.pos-nav-link`, `.nav-badge` | `css/layout/app-shell.css` |
| `@media (max-width: 980px)` | `.metric-strip`, `.workspace-grid`, `.cashier-layout`, `.kanban-grid` | `css/layout/app-shell.css` |
| `@media (max-width: 720px)` | `.pos-shell`, `.pos-rail`, `.pos-nav`, `.pos-main`, `.pos-topbar`, `.pos-content`, `.workspace-grid`, `.table-grid-redesign`, `.menu-chips` | `css/layout/app-shell.css` |

---

## Notes

- Media queries ที่เกี่ยวข้องกับหลายกลุ่ม (layout + grid + feature) จะถูกรวมไว้ใน `layout/app-shell.css` เพื่อป้องกัน cascade order issues
- Selectors ที่เป็น comma-separated ข้ามกลุ่ม (เช่น `.workspace-grid, .dashboard-panels, .settings-grid`) จะถูกแยกออกจากกันเมื่อย้ายไป target file
- ไฟล์ `dialog.css` ยังไม่มี — รอจนกว่ามี dialog component จริง
- ไฟล์ `layout/grid.css` ยังไม่มี — รอจนกว่ามี `.container`, `.row`, `.col` จริง