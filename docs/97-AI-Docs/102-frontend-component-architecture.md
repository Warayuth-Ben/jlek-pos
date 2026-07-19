# สถาปัตยกรรม Frontend Component — JLek POS

> **เอกสารออกแบบสำหรับนักพัฒนาและ AI Agent ทุกคนที่ทำงานกับ Blazor Components**

---

## 1. Purpose — ทำไมต้องมี Component Architecture?

หลังจาก CSS Architecture (Sprints B–M) เสร็จสมบูรณ์ ขั้นตอนต่อไปคือการจัดระเบียบ Blazor Components

### ปัญหาที่เกิดขึ้นในปัจจุบัน

- **Component หลายตัวยังอยู่ใน Page** — `CashierPage.razor`, `KitchenPage.razor` มี business logic ปนกับ UI
- **No clear separation** — ไม่รู้ว่า component ไหนเป็น Smart, ไหนเป็น Presentational
- **Shared vs Feature ไม่ชัดเจน** — `ToastNotification.razor` ใช้ได้ทุกหน้า แต่อยู่ใน `/Shared/` ขณะที่ `BillSummary.razor` ใช้เฉพาะ Cashier แต่อยู่ใน `/Components/Cashier/`
- **State management กระจัดกระจาย** — บาง state อยู่ใน component, บาง state อยู่ใน store, บาง state อยู่ใน service
- **ยากต่อการทดสอบ** — Component ที่มี business logic ปนกับ UI ไม่สามารถ unit test ได้ง่าย

### เป้าหมาย

| เป้าหมาย | คำอธิบาย |
|----------|----------|
| **Separation of Concerns** | Business logic แยกจาก UI presentation |
| **Reusability** | Shared components ใช้ได้ทุก feature โดยไม่ต้อง duplicate |
| **Testability** | Component ที่ logic บริสุทธิ์สามารถ unit test ได้ |
| **Maintainability** | แก้ component หนึ่ง โดยไม่กระทบ component อื่น |
| **Consistency** | Component ทุกตัวมีโครงสร้างและ naming ที่เหมือนกัน |

---

## 2. Component Philosophy — ปรัชญา

### หลักการสำคัญ

1. **Composition over duplication**  
   สร้าง component เล็ก ๆ แล้วประกอบกัน ดีกว่าสร้าง component ใหญ่ที่ทำทุกอย่าง

2. **Small reusable components**  
   Component ควรมีหน้าที่เดียว (Single Responsibility)  
   ✅ `MetricCard.razor` — แสดง metric card  
   ❌ `DashboardPage.razor` — รวมทุกอย่างไว้ในหน้าเดียว

3. **Business UI belongs to Features**  
   Component ที่ specific กับ feature ใด feature หนึ่ง ต้องอยู่ใน folder ของ feature นั้น  
   ✅ `Components/Cashier/OrderPanel.razor`  
   ❌ `Shared/OrderPanel.razor`

4. **Generic UI belongs to Shared**  
   Component ที่ reuse ได้ข้าม feature ต้องอยู่ใน Shared  
   ✅ `Shared/ConfirmDialog.razor`  
   ❌ `Cashier/ConfirmDialog.razor` (duplicate)

5. **No business logic in Presentational components**  
   Presentational component มีแค่ Parameters และ EventCallbacks  
   ไม่มีการเรียก API, ไม่มีการ inject service, ไม่มีการจัดการ state

---

## 3. Folder Structure — โครงสร้างโฟลเดอร์

```
JLek.POS.Web/
│
├── Components/                    # Feature-specific business components
│   ├── Cashier/
│   │   ├── TableGrid.razor
│   │   ├── OrderPanel.razor
│   │   ├── BillSummary.razor
│   │   ├── MenuModal.razor
│   │   ├── PaymentDialog.razor
│   │   ├── ReceiptPreview.razor
│   │   └── CashierToolbar.razor
│   │
│   ├── Kitchen/
│   │   ├── KitchenQueue.razor
│   │   ├── KitchenOrderCard.razor
│   │   ├── KitchenStatusBadge.razor
│   │   └── KitchenToolbar.razor
│   │
│   ├── Dashboard/
│   │   ├── MetricCard.razor
│   │   ├── LaunchCard.razor
│   │   └── QuickActions.razor
│   │
│   └── Reports/
│       ├── ReportFilters.razor
│       ├── SalesChart.razor
│       └── ExportPanel.razor
│
├── Shared/                        # Reusable cross-feature components
│   ├── ConfirmDialog.razor
│   ├── ToastNotification.razor
│   ├── LoadingSpinner.razor
│   ├── EmptyState.razor
│   ├── PageHeader.razor
│   ├── SectionTitle.razor
│   ├── SearchBox.razor
│   ├── DataTable.razor
│   ├── StatusBadge.razor
│   ├── SegmentedControl.razor
│   └── ActionBar.razor
│
├── Layout/                        # Application layout components
│   ├── MainLayout.razor
│   ├── NavMenu.razor
│   ├── WorkspaceShell.razor
│   └── LoadingContainer.razor
│
├── Pages/                         # Page components (routing targets)
│   ├── Home.razor
│   ├── Cashier/
│   │   └── CashierPage.razor
│   ├── Kitchen/
│   │   └── KitchenPage.razor
│   ├── Dashboard/
│   │   └── DashboardPage.razor
│   ├── Reports/
│   │   └── ReportsPage.razor
│   └── Settings/
│       └── SettingsPage.razor
│
├── Presentation/                  # Smart components, stores, state
│   ├── Platform/
│   ├── Cashier/
│   └── Context/
│
└── wwwroot/                       # Static files (CSS, JS, images)
```

### คำอธิบายแต่ละโฟลเดอร์

| โฟลเดอร์ | หน้าที่ |
|----------|--------|
| `Components/` | Business components ที่ specific กับ feature — ใช้เฉพาะใน feature นั้น |
| `Shared/` | Reusable components ที่ใช้ข้าม feature — generic UI |
| `Layout/` | Application shell — layout, nav, shell |
| `Pages/` | Page components — จุดเริ่มต้นสำหรับ routing |
| `Presentation/` | Smart components, stores, state management |

---

## 4. Component Hierarchy — ลำดับชั้น Component

```
App.razor
│
└── MainLayout.razor
    │
    ├── NavMenu.razor (Layout)
    │
    └── @Body
        │
        └── Page (e.g. CashierPage.razor)
            │
            ├── WorkspaceShell.razor (Layout)
            │
            ├── Feature Section
            │   ├── TableGrid.razor (Business)
            │   ├── OrderPanel.razor (Business)
            │   │   └── BillSummary.razor (Business)
            │   └── MenuModal.razor (Business)
            │
            ├── Shared Components
            │   ├── PageHeader.razor
            │   ├── SearchBox.razor
            │   └── SegmentedControl.razor
            │
            └── Primitive Components
                ├── Button.razor
                ├── Badge.razor
                └── Card.razor
```

**กฎการไหลของข้อมูล:**  
`Page → Feature Section → Shared Components → Primitive Components`

ข้อมูลไหลจากบนลงล่างเท่านั้น  
Events ไหลจากล่างขึ้นบนผ่าน `EventCallback`

---

## 5. Component Categories — ประเภทของ Component

### Primitive Components

Component พื้นฐานที่ reuse ได้มากที่สุด ไม่มี business logic

| Component | ตัวอย่าง |
|-----------|---------|
| `Button` | `.primary-action`, `.icon-button`, `.mini-button` |
| `Badge` | `.state-pill`, `.nav-badge` |
| `Card` | `.surface-panel`, `.metric-tile` |
| `Input` | `.date-input`, `.settings-form input` |
| `Divider` | เส้นแบ่ง |

**ที่อยู่:** CSS อยู่ที่ `components/` — Component อยู่ที่ `Shared/` (หรือ primitive library)

---

### Layout Components

Component ที่จัดการ structure และตำแหน่ง

| Component | หน้าที่ |
|-----------|--------|
| `PageHeader` | Title, subtitle, actions |
| `Section` | Content section with padding |
| `Panel` | Bordered container |
| `Grid` | Responsive grid layout |

**ที่อยู่:** `Shared/` (ถ้า reuse) หรือ `Layout/` (ถ้าเป็น shell)

---

### Business Components

Component ที่ specific กับ feature เฉพาะ

| Feature | Component |
|---------|-----------|
| Cashier | `TableGrid`, `OrderPanel`, `BillSummary`, `MenuModal`, `PaymentDialog` |
| Kitchen | `KitchenQueue`, `KitchenOrderCard`, `KitchenStatusBadge` |
| Dashboard | `MetricCard`, `LaunchCard` |
| Reports | `ReportFilters`, `SalesChart` |

**ที่อยู่:** `Components/{FeatureName}/`

---

### Page Components

Component ที่เป็นเป้าหมายของ router

| Page | Route |
|------|-------|
| `CashierPage` | `/cashier` |
| `KitchenPage` | `/kitchen` |
| `DashboardPage` | `/` |
| `ReportsPage` | `/reports` |
| `SettingsPage` | `/settings` |

**ที่อยู่:** `Pages/`

---

## 6. Smart vs Presentational Components — Component อัจฉริยะกับ Component แสดงผล

### Presentational Component

Component ที่ **ไม่มี business logic** — มีแค่ Parameters และ EventCallbacks

```razor
@* ✅ Presentational Component *@
@* MetricCard.razor *@
<div class="metric-tile">
    <span>@Label</span>
    <strong>@Value</strong>
</div>

@code {
    [Parameter] public string Label { get; set; } = "";
    [Parameter] public string Value { get; set; } = "";
}
```

**อนุญาตให้มี:**
- `[Parameter]` properties
- `[Parameter] public EventCallback<>` 
- CSS class binding
- Conditional rendering (`@if`, `@switch`)

**ห้ามมี:**
- การ inject service
- การเรียก API
- Business logic
- State management
- การ import `IJSRuntime`
- `OnInitializedAsync` ที่มี business logic

---

### Smart Component (Container Component)

Component ที่ **จัดการ business logic** — inject service, จัดการ state, เรียก API

```razor
@* ✅ Smart Component *@
@* CashierWorkspace.razor *@
@inject IOrderClient OrderClient

<TableGrid Tables="@tables"
           OnSelectTable="HandleSelectTable" />

@code {
    private List<TableResponse> tables = new();
    
    protected override async Task OnInitializedAsync()
    {
        tables = (await TableClient.GetAllAsync()).ToList();
    }
    
    private async Task HandleSelectTable(Guid tableId)
    {
        // Business logic here
    }
}
```

**อนุญาตให้มี:**
- `@inject` service
- State management
- API calls
- Business logic
- Error handling
- Loading states

**ห้ามมี:**
- CSS class definitions (ใช้ CSS layer แทน)
- Hardcoded strings ที่ควรเป็น Parameter

---

### กฎการแยก

```
Smart Component
│
├── จัดการ business logic
├── inject services
├── จัดการ state
│
└── ประกอบด้วย Presentational Components
    ├── ✅ MetricCard (ไม่มี logic)
    ├── ✅ Button (ไม่มี logic)
    └── ❌ ไม่ควรมี logic ในนี้
```

---

## 7. State Management — การจัดการ State

### ที่อยู่ของ State

| State Type | ควรอยู่ที่ไหน | ตัวอย่าง |
|------------|--------------|---------|
| UI State (local) | Component (`@code`) | `_isLoading`, `_selectedTab` |
| Page State | Page Component | `_tables`, `_orders` |
| Feature State | Store / Service | `CashierStore`, `CartState` |
| Application State | DI Service (Scoped) | `AuthState`, `WorkspaceContext` |
| Server State | API call → cache | `Product list`, `Table list` |

### หลักการ

1. **Local state มาก่อน** — เริ่มด้วย state ใน component ก่อน
2. **Lift state up** — เมื่อ component อื่นต้องการ share state → ย้ายขึ้นไป
3. **Store สำหรับ cross-component** — เมื่อหลาย component ใน feature เดียวกันต้องการ share state
4. **DI Service สำหรับ cross-feature** — เมื่อหลาย feature ต้องการ state เดียวกัน

### ตัวอย่างการเลือก

```razor
@* ✅ Local state — ไม่มี component อื่นใช้ *@
@code {
    private bool _isMenuOpen;
}

@* ✅ Parameter — parent เป็นเจ้าของ state *@
<BillSummary Items="@orderItems" />

@* ✅ Store — หลาย component ใน Cashier ใช้ *@
@inject CashierStore Store
```

### สิ่งที่ควรหลีกเลี่ยง

- ❌ Global static state
- ❌ CascadingParameter สำหรับ data ที่ไม่ใช่ theme/context
- ❌ Component สื่อสารกันผ่าน DOM โดยตรง

---

## 8. Component Communication — การสื่อสารระหว่าง Component

### รูปแบบที่ถูกต้อง

```
Parent Component
    │
    ├── [Parameter] — ส่งข้อมูลลงไป
    │
    └── @:EventCallback — รับ event จาก child
    
Child Component
    │
    ├── รับ Parameter จาก parent
    │
    └── ส่ง EventCallback กลับไป
```

### ตัวอย่าง

```razor
@* Parent *@
<TableGrid 
    Tables="@tables"
    OnSelectTable="@HandleSelectTable" />

@code {
    private async Task HandleSelectTable(Guid id)
    {
        // รับ event จาก child
    }
}

@* Child (TableGrid.razor) *@
@code {
    [Parameter] public List<TableResponse> Tables { get; set; } = new();
    [Parameter] public EventCallback<Guid> OnSelectTable { get; set; }
    
    private async Task SelectTable(Guid id)
    {
        await OnSelectTable.InvokeAsync(id);
    }
}
```

### กฎการสื่อสาร

1. **Parent → Child**: ผ่าน `[Parameter]` เท่านั้น
2. **Child → Parent**: ผ่าน `EventCallback` เท่านั้น
3. **Sibling → Sibling**: ต้องผ่าน parent component เท่านั้น — ห้าม sibling คุยกันตรง ๆ
4. **Cross-feature**: ผ่าน service / store เท่านั้น — ห้าม import component ของ feature อื่น

---

## 9. Naming Convention — ระบบการตั้งชื่อ

### Component Name

| รูปแบบ | ตัวอย่าง |
|--------|---------|
| PascalCase | `TableGrid`, `OrderPanel`, `KitchenTicket` |
| Feature prefix | `KitchenOrderCard`, `CashierToolbar` |
| No generic names | ❌ `Widget`, `Item`, `Component1` |

### File Name

- ตรงกับ class name ทุกประการ
- `.razor` extension สำหรับ component
- `.razor.cs` สำหรับ code-behind (ถ้าแยก)
- `.razor.css` สำหรับ scoped CSS (ถ้ามี)

### Parameter Name

- PascalCase
- Prefix ด้วย `On` สำหรับ EventCallback
- ตัวอย่าง: `@OnSelectTable`, `@OnConfirm`, `@OnCancel`

---

## 10. Component Ownership — ใครเป็นเจ้าของ Component ไหน

| Component | Owner | Folder |
|-----------|-------|--------|
| `TableGrid` | **Cashier** | `Components/Cashier/` |
| `OrderPanel` | **Cashier** | `Components/Cashier/` |
| `BillSummary` | **Cashier** | `Components/Cashier/` |
| `MenuModal` | **Cashier** | `Components/Cashier/` |
| `PaymentDialog` | **Cashier** | `Components/Cashier/` |
| `ReceiptPreview` | **Cashier** | `Components/Cashier/` |
| `KitchenQueue` | **Kitchen** | `Components/Kitchen/` |
| `KitchenOrderCard` | **Kitchen** | `Components/Kitchen/` |
| `KitchenStatusBadge` | **Kitchen** | `Components/Kitchen/` |
| `MetricCard` | **Dashboard** | `Components/Dashboard/` |
| `LaunchCard` | **Dashboard** | `Components/Dashboard/` |
| `ReportFilters` | **Reports** | `Components/Reports/` |

### กฎ Ownership

1. **Feature Component = Feature Owner** — มีเฉพาะใน feature นั้น
2. **Shared Component = Project Owner** — ใช้ได้ทุก feature
3. **Layout Component = App Owner** — เฉพาะ app shell
4. ห้าม Feature Component import Component ของ Feature อื่น

---

## 11. Reuse Guidelines — แนวทางการ Reuse Component

### Decision Flow

```
คุณกำลังจะสร้าง Component ใหม่
│
├─ Component นี้ถูกใช้โดย 2+ features หรือไม่?
│  ├─ ใช่ → ไปที่ Shared/
│  └─ ไม่ → ไปต่อ
│
├─ Component นี้มี business logic เฉพาะ feature หรือไม่?
│  ├─ ใช่ → ไปที่ Components/{FeatureName}/
│  └─ ไม่ → ไปต่อ
│
├─ Component นี้เป็น primitive UI หรือไม่? (button, card, badge)
│  ├─ ใช่ → ใช้ CSS class จาก components/ layer + Shared/ component
│  └─ ไม่ → ไปต่อ
│
└─ Component นี้เป็น layout หรือไม่?
   ├─ ใช่ → Layout/
   └─ ไม่ → กลับไปทบทวน
```

### ตัวอย่าง

**สถานการณ์:** ต้องการ `ConfirmDialog` สำหรับยืนยันการลบ order (Cashier) และยืนยันการ serve (Kitchen)

```
ถาม: ConfirmDialog ใช้กี่ feature?
ตอบ: 2 features → ต้องไป Shared/

ผลลัพธ์: Shared/ConfirmDialog.razor
```

**สถานการณ์:** ต้องการ `KitchenOrderCard` สำหรับแสดง ticket ใน Kitchen

```
ถาม: KitchenOrderCard ใช้กี่ feature?
ตอบ: 1 feature (Kitchen)
ถาม: มี business logic เฉพาะ Kitchen หรือไม่?
ตอบ: ใช่ — แสดงสถานะ, ปุ่ม start/complete/serve

ผลลัพธ์: Components/Kitchen/KitchenOrderCard.razor
```

---

## 12. Future Component Candidates — Component ที่ควรมีในอนาคต

| Component | Category | Priority | ปัจจุบันมีหรือยัง? |
|-----------|----------|----------|-------------------|
| `PageHeader` | Layout | สูง | ❌ ยังไม่มี |
| `MetricCard` | Dashboard | สูง | ✅ มี (เป็น CSS) |
| `StatCard` | Dashboard | กลาง | ❌ ยังไม่มี |
| `EmptyState` | Shared | สูง | ✅ มี (เป็น CSS) |
| `LoadingIndicator` | Shared | สูง | ❌ ยังไม่มี |
| `ConfirmDialog` | Shared | สูง | ✅ มี — ต้องย้ายไป Shared/ |
| `ToastNotification` | Shared | สูง | ✅ มีแล้ว |
| `DataTable` | Shared | สูง | ❌ ยังไม่มี |
| `SearchBox` | Shared | กลาง | ❌ ยังไม่มี |
| `FilterBar` | Reports | กลาง | ❌ ยังไม่มี |
| `SectionTitle` | Layout | ต่ำ | ❌ ยังไม่มี |
| `ActionBar` | Layout | ต่ำ | ❌ ยังไม่มี |
| `StatusBadge` | Kitchen | สูง | ✅ มี — ต้องย้ายไป Shared/ |
| `SegmentedControl` | Shared | สูง | ✅ มี (ใน app.css) |
| `OrderBuilder` | Cashier | สูง | ✅ มี — ต้อง refactor |
| `BillSummary` | Cashier | สูง | ✅ มีแล้ว |
| `ReceiptPreview` | Cashier | สูง | ✅ มีแล้ว |
| `SalesChart` | Reports | กลาง | ❌ ยังไม่มี |
| `ExportPanel` | Reports | ต่ำ | ❌ ยังไม่มี |
| `SettingsForm` | Settings | ต่ำ | ✅ มี (เป็นหน้า) |

---

## 13. Migration Strategy — วิธีการย้าย Component

### หลักการ

ย้ายทีละ component โดยไม่มีการเปลี่ยนแปลง visual

### ขั้นตอน

1. **วิเคราะห์** — Component นี้ควรอยู่ที่ไหน? (Shared, Feature, Layout)
2. **สร้าง** — สร้าง component ไฟล์ใหม่ในตำแหน่งที่ถูกต้อง
3. **ย้าย logic** — ย้าย logic จาก Page มาไว้ใน component
4. **ย้อนกลับ** — ตรวจสอบว่า UI ยังเหมือนเดิม
5. **Build** — `dotnet build` ต้องผ่าน 0 errors
6. **Commit** — commit ทีละ component

### ตัวอย่างการย้าย

**ก่อน:**
```
CashierPage.razor (565 บรรทัด — รวมทุกอย่าง)
```

**หลัง:**
```
CashierPage.razor (~50 บรรทัด — แค่ประกอบ component)
├── Components/Cashier/TableGrid.razor
├── Components/Cashier/OrderPanel.razor
├── Components/Cashier/BillSummary.razor
├── Components/Cashier/MenuModal.razor
├── Components/Cashier/PaymentDialog.razor
├── Components/Cashier/ReceiptPreview.razor
├── Shared/ConfirmDialog.razor
└── Shared/ToastNotification.razor
```

### กฎ

- **No visual changes** — UI ต้องเหมือนเดิมทุกประการ
- **One component at a time** — ย้ายทีละ component เท่านั้น
- **Build must pass** — ทุก commit ต้อง build ผ่าน
- **CSS changes are separate** — ใช้ CSS Architecture ที่มีอยู่ ไม่ต้องแก้

---

## 14. Review Checklist — Checklist สำหรับ Merge

ก่อน merge component ใหม่ เข้า main branch

### Architecture

- [ ] Component อยู่ใน folder ที่ถูกต้อง (Shared / Feature / Layout)
- [ ] Component ไม่มี dependency ข้าม feature
- [ ] Component ไม่ duplicate component ที่มีอยู่แล้ว

### Code Quality

- [ ] Presentational component มีแค่ `[Parameter]` และ `EventCallback`
- [ ] Smart component ไม่มีการ render logic ปนกับ business logic
- [ ] ไม่มี hardcoded strings — ใช้ `[Parameter]` หรือ localization
- [ ] Error handling ถูกต้อง — ไม่ swallow exception

### CSS

- [ ] CSS class ใช้จาก CSS Architecture layer (ไม่ใช่ inline style)
- [ ] ไม่มี CSS อยู่ใน component `.razor` — ใช้ `.razor.css` หรือ CSS layer
- [ ] Scoped CSS (`.razor.css`) สำหรับ component-specific เท่านั้น

### Naming

- [ ] Component name เป็น PascalCase
- [ ] ไม่มี generic names (`Component1`, `Widget`)
- [ ] Parameter names เป็น PascalCase
- [ ] EventCallback prefix ด้วย `On`

### Build

- [ ] `dotnet build` ผ่าน 0 errors
- [ ] ไม่มี warnings ใหม่ (pre-existing warnings ยอมรับได้)

### Testing

- [ ] Component สามารถ render โดยไม่มี exception
- [ ] Loading / Empty / Error states ถูกจัดการ

---

## 15. Conclusion — สรุป

Frontend Component Architecture ของ JLek POS ออกแบบมาเพื่อ

```
Composition > Duplication
Small Components > Big Monoliths
Smart / Presentational Separation > Mixed Logic
Feature Ownership > Global State
```

**หลักการสำคัญ**

1. **Presentational components มี logic เท่าที่จำเป็น** — ถ้ามี business logic แสดงว่าควรเป็น Smart component
2. **Component รู้ที่อยู่ของตัวเอง** — Shared, Feature, Layout แต่ละที่มีที่
3. **No cross-feature dependencies** — Feature component ห้าม import component ของ feature อื่น
4. **Parent → Child → EventCallback** — การสื่อสารมีทิศทางเดียว
5. **Migrate ทีละ component** — ไม่มีการเปลี่ยนแปลงครั้งใหญ่

---

*อัปเดตล่าสุด: 19 กรกฎาคม 2026*