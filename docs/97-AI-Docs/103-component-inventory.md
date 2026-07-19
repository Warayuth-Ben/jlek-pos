# Component Inventory — JLek POS

> **เอกสารวิเคราะห์ Component ทั้งหมดในโปรเจกต์ ก่อนเริ่ม migration**

---

## 1. Summary

| Metric | Count |
|--------|-------|
| Total Razor files | 18 |
| Existing Components | 10 (ใน `Components/`, `Shared/`, `Presentation/`) |
| Page Components | 6 (ใน `Pages/`) |
| Candidate Components | 18 (ที่ควรแยกออกมา) |
| Migration Phases | 5 |
| Highest Risk Page | **CashierPage** (~565 บรรทัด, business logic + UI ปนกัน) |
| Largest Razor Page | **CashierWorkspace.razor** หรือ **CashierPage.razor** |
| Technical Debt Items | 7 |

---

## 2. Existing Components

### Shared Components

| Component | Path | Type | ปัจจุบัน |
|-----------|------|------|---------|
| `ToastNotification` | `Components/Shared/ToastNotification.razor` | Presentational | ✅ ใช้ได้หลายหน้า |
| `ConfirmDialog` | `Components/Shared/ConfirmDialog.razor` | Presentational | ✅ ใช้ได้หลายหน้า |
| `ToastType` | `Components/Shared/ToastType.cs` | Enum | ✅ ใช้ร่วมกับ Toast |

### Cashier Components

| Component | Path | Type | ปัจจุบัน |
|-----------|------|------|---------|
| `TableGrid` | `Components/Cashier/TableGrid.razor` | Business | ✅ Feature-specific |
| `OrderPanel` | `Components/Cashier/OrderPanel.razor` | Business | ✅ Feature-specific |
| `BillSummary` | `Components/Cashier/BillSummary.razor` | Business | ✅ Feature-specific |
| `MenuModal` | `Components/Cashier/MenuModal.razor` | Business | ✅ Feature-specific |
| `PaymentDialog` | `Components/Cashier/PaymentDialog.razor` | Business | ✅ Feature-specific |
| `ReceiptPreview` | `Components/Cashier/ReceiptPreview.razor` | Business | ✅ Feature-specific |

### Kitchen Components

| Component | Path | Type | ปัจจุบัน |
|-----------|------|------|---------|
| `KitchenStatusBadge` | `Components/Kitchen/KitchenStatusBadge.razor` | Business | ✅ Feature-specific |
| `KitchenOrderCard` | `Components/Kitchen/KitchenOrderCard.razor` | Business | ✅ Feature-specific |
| `KitchenToolbar` | `Components/Kitchen/KitchenToolbar.razor` | Business | ✅ Feature-specific |
| `KitchenQueue` | `Components/Kitchen/KitchenQueue.razor` | Business | ✅ Feature-specific |

### Presentation Components

| Component | Path | Type | ปัจจุบัน |
|-----------|------|------|---------|
| `WorkspaceShell` | `Presentation/Components/WorkspaceShell.razor` | Layout | ✅ App shell |
| `LoadingContainer` | `Presentation/Components/LoadingContainer.razor` | Layout | ✅ App shell |
| `CashierStore` | `Presentation/Cashier/CashierStore.cs` | Smart | ✅ State management |
| `CashierPresentationModels` | `Presentation/Cashier/CashierPresentationModels.cs` | Model | ✅ DTO |

### Layout Components

| Component | Path | Type | ปัจจุบัน |
|-----------|------|------|---------|
| `MainLayout` | `Layout/MainLayout.razor` | Layout | ✅ App shell |
| `NavMenu` | `Layout/NavMenu.razor` | Layout | ✅ App shell |

---

## 3. Page Components

| Page | Path | ประมาณบรรทัด | ปัญหา |
|------|------|-------------|-------|
| `CashierPage` | `Pages/Cashier/CashierPage.razor` | 565+ | Business logic ปน UI, state management ซับซ้อน |
| `CashierWorkspace` | `Pages/Cashier/CashierWorkspace.razor` | ~300 | มี business logic, API calls, state |
| `KitchenPage` | `Pages/Kitchen/KitchenPage.razor` | ~200 | มี business logic ปน UI |
| `DashboardPage` | `Pages/Dashboard/DashboardPage.razor` | ~120 | ส่วนใหญ่ reuse component แล้ว |
| `ReportsPage` | `Pages/Reports/ReportsPage.razor` | ~100 | เล็กที่สุด |
| `SettingsPage` | `Pages/Settings/SettingsPage.razor` | ~80 | เล็กที่สุด |
| `Home` | `Pages/Home.razor` | ~50 | Simple |

**Highest Risk Page:** `CashierPage.razor` — Business logic, state, API calls, และ UI rendering ปนกันในไฟล์ใหญ่

---

## 4. Candidate Components

### Phase 1 — Primitive Components (High Priority, Low Complexity)

| # | Component | Category | ปัจจุบัน | สร้างใหม่? |
|---|-----------|----------|---------|-----------|
| 1 | `Button` | Primitive | ✅ มี CSS class (`components/button.css`) | ✅ สร้าง Blazor component |
| 2 | `Badge` | Primitive | ✅ มี CSS class (`components/badge.css`) | ✅ สร้าง Blazor component |
| 3 | `Card` | Primitive | ✅ มี CSS class (`components/card.css`) | ✅ สร้าง Blazor component |
| 4 | `SegmentedControl` | Shared | ✅ มี (ใน app.css) | ✅ สร้าง Blazor component |
| 5 | `EmptyState` | Shared | ✅ มี CSS (`.empty-state` ใน `cashier.css`) | ✅ สร้าง Blazor component |

### Phase 2 — Layout Components (High Priority, Medium Complexity)

| # | Component | Category | ปัจจุบัน | สร้างใหม่? |
|---|-----------|----------|---------|-----------|
| 6 | `PageHeader` | Layout | ❌ ยังไม่มี — ทุกหน้ามี `<h1>` + description ซ้ำกัน | ✅ ใหม่ |
| 7 | `SectionTitle` | Layout | ❌ ยังไม่มี | ✅ ใหม่ |
| 8 | `MetricCard` | Dashboard | ✅ มี CSS (`.metric-tile`) | ✅ สร้าง Blazor component |
| 9 | `StatCard` | Dashboard | ❌ ยังไม่มี | ✅ ใหม่ |

### Phase 3 — Business Components (Medium Priority, High Complexity)

| # | Component | Category | ปัจจุบัน | สร้างใหม่? |
|---|-----------|----------|---------|-----------|
| 10 | `ToastNotification` → Shared | Shared | ✅ อยู่ที่ `Components/Shared/` (ถูกแล้ว) | ✅ ยืนยัน |
| 11 | `ConfirmDialog` → Shared | Shared | ✅ อยู่ที่ `Components/Shared/` (ถูกแล้ว) | ✅ ยืนยัน |
| 12 | `LoadingSpinner` | Shared | ❌ ยังไม่มี — `LoadingContainer` อยู่ที่ `Presentation/` | ✅ ย้าย/สร้าง |
| 13 | `SearchBox` | Shared | ❌ ยังไม่มี | ✅ ใหม่ |
| 14 | `DataTable` | Shared | ❌ ยังไม่มี — ทุก feature ต้องมีข้อมูลเป็นตาราง | ✅ ใหม่ |

### Phase 4 — Feature Business Components (Medium Priority, Medium Complexity)

| # | Component | Category | ปัจจุบัน | สร้างใหม่? |
|---|-----------|----------|---------|-----------|
| 15 | `OrderBuilder` | Cashier | ✅ มีย่อยแล้ว (OrderPanel, BillSummary) | ✅ Refactor |
| 16 | `CashierToolbar` | Cashier | ❌ ยังไม่มี — segmented control + actions | ✅ ใหม่ |
| 17 | `OrderLineItem` | Cashier | ✅ ซ้ำใน OrderPanel (`.order-line`) | ✅ Extract |
| 18 | `ReportFilters` | Reports | ❌ ยังไม่มี | ✅ ใหม่ |

### Phase 5 — Infrastructure Components (Low Priority, Medium Complexity)

| # | Component | Category | ปัจจุบัน | สร้างใหม่? |
|---|-----------|----------|---------|-----------|
| 19 | `KitchenTicketCard` | Kitchen | ✅ มี `KitchenOrderCard` | ✅ Rename |
| 20 | `BillSummaryItem` | Cashier | ✅ อยู่ใน `BillSummary` | ✅ Extract |

---

## 5. Component Inventory Table

### Inventory Table

| Component | Category | Current Pages | Shared? | Priority | Complexity | Risk | Status |
|-----------|----------|--------------|---------|----------|------------|------|--------|
| Button | Primitive | All | ✅ Shared | High | Low | Low | 🆕 Extract |
| Badge | Primitive | All | ✅ Shared | High | Low | Low | 🆕 Extract |
| Card | Primitive | Dashboard, Kitchen, Cashier | ✅ Shared | High | Low | Low | 🆕 Extract |
| SegmentedControl | Shared | Cashier, Reports | ✅ Shared | High | Low | Low | 🆕 Extract |
| EmptyState | Shared | Cashier | ✅ Shared | High | Low | Low | 🆕 Extract |
| PageHeader | Layout | Cashier, Kitchen, Dashboard | ✅ Shared | High | Low | Low | 🆕 New |
| SectionTitle | Layout | All pages | ✅ Shared | Medium | Low | Low | 🆕 New |
| MetricCard | Dashboard | Dashboard | ❌ Feature | High | Low | Low | 🆕 Extract |
| StatCard | Dashboard | Dashboard | ❌ Feature | Medium | Low | Low | 🆕 New |
| ToastNotification | Shared | Cashier, Kitchen | ✅ Shared | High | Low | Low | ✅ Existing |
| ConfirmDialog | Shared | Cashier, Kitchen | ✅ Shared | High | Low | Low | ✅ Existing |
| LoadingSpinner | Shared | All | ✅ Shared | High | Low | Low | 🆕 Extract |
| SearchBox | Shared | Cashier (menu) | ✅ Shared | Medium | Medium | Low | 🆕 New |
| DataTable | Shared | Reports, Dashboard | ✅ Shared | Medium | High | Medium | 🆕 New |
| OrderBuilder | Cashier | Cashier | ❌ Feature | Medium | Medium | Medium | 🔧 Refactor |
| CashierToolbar | Cashier | Cashier | ❌ Feature | Medium | Low | Low | 🆕 New |
| OrderLineItem | Cashier | Cashier | ❌ Feature | Medium | Low | Low | 🆕 Extract |
| ReportFilters | Reports | Reports | ❌ Feature | Medium | Medium | Low | 🆕 New |
| KitchenTicketCard | Kitchen | Kitchen | ❌ Feature | Medium | Low | Low | 🔧 Rename |
| BillSummaryItem | Cashier | Cashier | ❌ Feature | Low | Low | Low | 🆕 Extract |

---

## 6. Dependency Graph

```
App.razor
└── MainLayout.razor
    ├── NavMenu.razor
    │   └── Badge.razor (candidate)
    └── @Body
        │
        ├── CashierPage.razor [HIGH RISK — 565+ lines]
        │   └── WorkspaceShell.razor
        │       ├── PageHeader.razor (candidate)
        │       ├── TableGrid.razor
        │       │   └── StatePill / Badge.razor (candidate)
        │       ├── OrderPanel.razor
        │       │   ├── BillSummary.razor
        │       │   │   └── BillSummaryItem.razor (candidate)
        │       │   └── OrderLineItem.razor (candidate)
        │       ├── MenuModal.razor
        │       │   ├── SearchBox.razor (candidate)
        │       │   └── ProductCard.razor (candidate)
        │       ├── PaymentDialog.razor
        │       └── ReceiptPreview.razor
        │
        ├── KitchenPage.razor
        │   ├── PageHeader.razor (candidate)
        │   ├── KitchenToolbar.razor
        │   ├── KitchenQueue.razor
        │   │   ├── KitchenOrderCard.razor
        │   │   │   └── KitchenStatusBadge.razor
        │   │   │   └── Button (candidate)
        │   │   └── KitchenOrderCard.razor
        │   └── SegmentedControl.razor (candidate)
        │
        ├── DashboardPage.razor
        │   ├── PageHeader.razor (candidate)
        │   ├── MetricCard.razor (candidate)
        │   ├── StatCard.razor (candidate)
        │   └── MetricStrip (candidate)
        │
        ├── ReportsPage.razor
        │   ├── PageHeader.razor (candidate)
        │   ├── ReportFilters.razor (candidate)
        │   ├── DataTable.razor (candidate)
        │   └── SegmentedControl.razor (candidate)
        │
        └── SettingsPage.razor
            ├── PageHeader.razor (candidate)
            └── SettingsForm (feature component)
```

---

## 7. Technical Debt

| # | Issue | Location | Severity |
|---|-------|----------|----------|
| 1 | Business logic ปนกับ UI rendering | `CashierPage.razor` | 🔴 High |
| 2 | State management กระจัดกระจาย | หลายที่ใน Cashier | 🔴 High |
| 3 | เรียก API ใน Page component โดยตรง | `CashierPage.razor` | 🟡 Medium |
| 4 | Markup ซ้ำ — PageHeader | ทุกหน้า | 🟡 Medium |
| 5 | Markup ซ้ำ — EmptyState | `CashierPage`, `MenuModal` | 🟡 Medium |
| 6 | Scoped CSS ปนกับ global CSS | NavMenu.razor.css, MainLayout.razor.css | 🟢 Low |
| 7 | Component naming ไม่ consistent | `CamelCase` vs `PascalCase` บางที่ | 🟢 Low |

---

## 8. Migration Order (5 Phases)

### Phase 1 — Foundation (Priority: Critical, Complexity: Low)

**Duration:** ~1-2 sprints
**Risk:** Low

1. `Button.razor` — Wrapper สำหรับ CSS classes
2. `Badge.razor` — Wrapper สำหรับ `.state-pill`, `.nav-badge`
3. `Card.razor` — Wrapper สำหรับ `.surface-panel`, `.metric-tile`
4. `SegmentedControl.razor` — จาก `app.css`
5. `EmptyState.razor` — จาก `.empty-state`
6. `LoadingSpinner.razor` — จาก `LoadingContainer`

### Phase 2 — Shared Layout (Priority: High, Complexity: Low)

**Duration:** ~1 sprint
**Risk:** Low

7. `PageHeader.razor` — `@Title`, `@Subtitle`, `@Actions` (ใช้ทุกหน้า)
8. `SectionTitle.razor` — `@Title`
9. `SearchBox.razor` — `@SearchTerm`, `@OnSearch`

### Phase 3 — Business Shared (Priority: Medium, Complexity: Medium)

**Duration:** ~2 sprints
**Risk:** Medium

10. `MetricCard.razor` — `@Label`, `@Value`, `@Trend`
11. `StatCard.razor` — `@Label`, `@Value`, `@Variant`
12. `DataTable.razor` — `@Items`, `@Columns`, `@OnRowClick`

### Phase 4 — Feature Migration (Priority: Medium, Complexity: High)

**Duration:** ~3 sprints
**Risk:** High (ต้องไม่กระทบ UI)

13. Refactor `CashierPage` → ใช้ component
14. Refactor `OrderPanel` → แยก `OrderLineItem`
15. Refactor `BillSummary` → แยก `BillSummaryItem`
16. Rename `KitchenOrderCard` → `KitchenTicketCard`
17. สร้าง `CashierToolbar`, `ReportFilters`

### Phase 5 — Polish (Priority: Low, Complexity: Medium)

**Duration:** ~1 sprint
**Risk:** Low

18. Rename components ให้ consistent
19. เพิ่ม Review Checklist (จาก 102-frontend-component-architecture.md)
20. เพิ่ม Unit tests สำหรับ Presentational components

---

## 9. Top 10 Components ที่ควรแยกก่อน

| Rank | Component | เหตุผล |
|------|-----------|--------|
| 1 | **Button** | ใช้ทุกหน้า — primitive ที่สุด, reuse มากที่สุด |
| 2 | **Badge** | ใช้ทุกหน้า — `.state-pill`, `.nav-badge` |
| 3 | **SegmentedControl** | ใช้หลายหน้า — Cashier (category), Reports (tabs) |
| 4 | **EmptyState** | ใช้ซ้ำ — Cashier (no orders), Menu (no results) |
| 5 | **PageHeader** | markup ซ้ำกัน 6 หน้า — คุ้มค่าที่สุด |
| 6 | **LoadingSpinner** | ใช้ทุกหน้า — แทน `LoadingContainer` |
| 7 | **MetricCard** | Dashboard หลัก — reuse หลายตำแหน่ง |
| 8 | **SearchBox** | ใช้ใน MenuModal — จะใช้ใน Reports ด้วย |
| 9 | **DataTable** | Reports + Dashboard — reuse ข้าม feature |
| 10 | **OrderLineItem** | Extract จาก `OrderPanel` — component ใหญ่เกินไป |

---

## 10. รายละเอียด Component Candidate

### Candidate 1: Button

```razor
@* Shared/Button.razor *@
<button class="@Class" disabled="@Disabled" @onclick="@OnClick">
    @ChildContent
</button>

@code {
    [Parameter] public string Class { get; set; } = "";
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

### Candidate 2: Badge

```razor
@* Shared/Badge.razor *@
<span class="state-pill @VariantClass">
    @ChildContent
</span>

@code {
    [Parameter] public string Variant { get; set; } = "";
    private string VariantClass => Variant switch {
        "occupied" => "occupied",
        _ => ""
    };
}
```

### Candidate 3: SegmentedControl

```razor
@* Shared/SegmentedControl.razor *@
<div class="segmented-control">
    @foreach (var item in Items)
    {
        <button class="@(item.IsActive ? "active" : "")"
                @onclick="() => OnSelect.InvokeAsync(item.Value)">
            @item.Label
        </button>
    }
</div>
```

### Candidate 4: EmptyState

```razor
@* Shared/EmptyState.razor *@
<div class="empty-state">
    <p>@Message</p>
</div>

@code {
    [Parameter] public string Message { get; set; } = "ไม่มีข้อมูล";
}
```

### Candidate 5: PageHeader

```razor
@* Shared/PageHeader.razor *@
<div class="workspace-heading">
    <div>
        <p class="eyebrow">@Kicker</p>
        <h1>@Title</h1>
    </div>
    @Actions
</div>

@code {
    [Parameter] public string Title { get; set; } = "";
    [Parameter] public string Kicker { get; set; } = "";
    [Parameter] public RenderFragment? Actions { get; set; }
}
```

### Candidate 6: MetricCard

```razor
@* Components/Dashboard/MetricCard.razor *@
<div class="metric-tile">
    <span>@Label</span>
    <strong>@Value</strong>
</div>

@code {
    [Parameter] public string Label { get; set; } = "";
    [Parameter] public string Value { get; set; } = "";
}
```

---

## 11. Key Findings

### Repeated Markup Patterns

1. **PageHeader** — `workspace-heading` + `h1` + `eyebrow` + actions **ซ้ำทุกหน้า**
2. **SegmentedControl** — `segmented-control` + `button.active` **ซ้ำใน Cashier และ Reports**
3. **EmptyState** — `.empty-state` + "ไม่มีข้อมูล" **ซ้ำใน Cashier**
4. **MetricTile** — `.metric-tile` > `span` + `strong` **ซ้ำใน Dashboard**
5. **LoadingContainer** — มีใน Cashier Workspace → ควรเป็น Shared

### Repeated CSS Classes (already extracted)

- `.primary-action` → `components/button.css` ✅
- `.surface-panel` → `components/card.css` ✅
- `.state-pill` → `components/badge.css` ✅
- `.segmented-control` → ยังอยู่ใน `app.css` ❌

### File Size Issues

| File | Estimated Lines | Issue |
|------|----------------|-------|
| `CashierPage.razor` + `CashierWorkspace.razor` | ~800+ | 🔴 Business logic + UI ปนกัน |
| `OrderPanel.razor` | ~200 | Contains `OrderLine` + `BillSummary` |
| `KitchenPage.razor` | ~200 | Medium — ควรแยกเพิ่ม |

---

## 12. Recommendations

1. **Phase 1 ทันที** — Primitive components (Button, Badge, SegmentedControl) = low risk, high value
2. **Phase 2 ก่อน Cashier refactor** — PageHeader, EmptyState = ลด markup ซ้ำ
3. **Cashier refactor คือความเสี่ยงสูงสุด** — ต้องวางแผนดี, test ทุกครั้ง
4. **Keep existing components** — อย่าสร้างใหม่ถ้ามีแล้ว (เช่น Toast, ConfirmDialog)
5. **No CSS changes** — ใช้ CSS Architecture ที่มีอยู่, ไม่ต้องแก้ CSS

---

*อัปเดตล่าสุด: 19 กรกฎาคม 2026*