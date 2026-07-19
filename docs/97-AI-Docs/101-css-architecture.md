# สถาปัตยกรรม CSS — JLek POS

> **เอกสารอ้างอิงสำหรับนักพัฒนาทุกคนที่ทำงานกับ CSS ในโปรเจกต์ JLek POS**

---

## 1. Purpose — ทำไมต้องมี CSS Architecture?

ก่อนการปรับโครงสร้าง (Sprints B–M)  **CSS ทั้งหมด 565 บรรทัดอยู่ในไฟล์เดียว** `wwwroot/css/app.css`

ปัญหาที่เกิดขึ้น

- **ยากต่อการบำรุงรักษา** — ต้องเลื่อนหาตัว selector ท่ามกลาง selector อื่น ๆ นับร้อย
- **ownership ไม่ชัดเจน** — ไม่รู้ว่า selector ไหนเป็นของ feature ไหน
- **cross-feature coupling** — selector กลุ่มเดียวถูกใช้โดยหลาย feature
- **merge conflict สูง** — ทุกคนแก้ไขไฟล์เดียวกัน
- **ไม่สามารถลบ selector ที่ไม่ใช้แล้วได้** — ไม่รู้ว่าปลอดภัยหรือไม่

CSS Architecture ถูกออกแบบมาเพื่อแก้ปัญหาเหล่านี้

**เป้าหมาย**

| เป้าหมาย | คำอธิบาย |
|----------|----------|
| **Maintainability** | แก้ไข CSS ได้โดยไม่กระทบส่วนอื่น |
| **Scalability** | เพิ่ม feature ใหม่โดยไม่ทำให้โครงสร้างพัง |
| **Separation of Concerns** | แต่ละ layer มีหน้าที่ชัดเจน |
| **Feature Ownership** | Feature หนึ่งเป็นเจ้าของ CSS ของตัวเอง |
| **Safe Refactoring** | ลบ selector ที่ไม่ใช้แล้วได้อย่างมั่นใจ |

---

## 2. Folder Structure — โครงสร้างโฟลเดอร์

```
wwwroot/css/
│
├── app.css                        # Entry point — import ทั้งหมดเท่านั้น
│
├── foundation/                    # รากฐานของระบบ
│   ├── variables.css              # Design tokens
│   ├── reset.css                  # Base reset
│   ├── typography.css             # Typography
│   └── utilities.css              # Utility classes (เตรียมไว้)
│       animation.css              # Animations (เตรียมไว้)
│
├── components/                    # Reusable UI components
│   ├── button.css                 # ปุ่มทุกประเภท
│   ├── card.css                   # Surface, card containers
│   ├── badge.css                  # Status badges, notification badges
│   ├── form.css                   # Form components (เตรียมไว้)
│   ├── table.css                  # Table styles (เตรียมไว้)
│       toast.css                  # Toast (เตรียมไว้)
│
├── layout/                        # Application shell layout
│   ├── app-shell.css              # Shell layout (grid structure)
│   ├── header.css                 # Topbar layout (เตรียมไว้)
│   ├── sidebar.css                # Sidebar layout (เตรียมไว้)
│   └── navigation.css             # Navigation layout (เตรียมไว้)
│
└── features/                      # Business feature styles
    ├── cashier/                   # หน้าร้าน
    │   ├── cashier.css            # Cashier feature
    │   ├── product-grid.css       # Cashier sub-component
    │   ├── order-summary.css      # Cashier sub-component
    │   └── payment-panel.css      # Cashier sub-component
    ├── kitchen/                   # ครัว
    │   ├── kitchen.css            # Kitchen feature
    │   ├── kitchen-board.css      # Kitchen sub-component
    │   ├── kitchen-column.css     # Kitchen sub-component
    │   └── kitchen-ticket.css     # Kitchen sub-component
    ├── dashboard/                 # หน้าแดชบอร์ด
    │   └── dashboard.css          # Dashboard feature
    ├── reports/                   # หน้ารายงาน
    │   └── reports.css            # Reports feature
    └── settings/                  # หน้าการตั้งค่า
        └── settings.css           # Settings feature
```

### หมายเหตุ

- `app.css` **ไม่มี selectors แล้ว** (เหลือแค่ 12 `@import` และ media queries)
- โฟลเดอร์ `foundation/` มี `utilities.css`, `animation.css` เตรียมไว้สำหรับอนาคต
- Feature sub-component files (เช่น `product-grid.css`) ถูก import โดย `cashier.css` ไม่ใช่ `app.css`

---

## 3. Layer Responsibilities — หน้าที่ของแต่ละ Layer

### Foundation Layer (`foundation/`)

**ความรับผิดชอบ**
- กำหนด design tokens (`variables.css`)
- reset ค่าเริ่มต้นของเบราว์เซอร์ (`reset.css`)
- กำหนด typography ทั่วไป (`typography.css`)
- utility classes และ animations

**อนุญาตให้ depend บน**
- ไม่มี — foundation ต้องเป็นอิสระ

**ห้าม depend บน**
- components, layout, features ทุกอย่าง

---

### Components Layer (`components/`)

**ความรับผิดชอบ**
- Reusable UI components ที่ใช้ได้หลายหน้า
- ปุ่ม (`button.css`)
- การ์ด (`card.css`)
- Badge (`badge.css`)

**อนุญาตให้ depend บน**
- Foundation (variables, reset, typography)

**ห้าม depend บน**
- Layout
- Features (ห้ามเด็ดขาด)

---

### Layout Layer (`layout/`)

**ความรับผิดชอบ**
- Application shell structure
- จัดวาง element หลักบนหน้า
- กำหนด grid, min-height, max-width

**อนุญาตให้ depend บน**
- Foundation
- Components

**ห้าม depend บน**
- Features

---

### Features Layer (`features/`)

**ความรับผิดชอบ**
- Business UI ที่เฉพาะเจาะจงต่อ feature นั้น
- Cashier, Kitchen, Dashboard, Reports, Settings
- Feature sub-components

**อนุญาตให้ depend บน**
- Foundation
- Components
- Layout

**ห้าม depend บน**
- Feature อื่น (ห้าม `cashier.css` import `kitchen.css`)

---

## 4. Dependency Direction — ทิศทางการอ้างอิง

```
          Foundation
              ↓
          Components
              ↓
            Layout
              ↓
           Features
```

**กฎเหล็ก**

1. **Higher layers** อาจ depend บน **lower layers**
2. **Lower layers** ห้าม depend บน **higher layers**

ตัวอย่างที่ถูกต้อง

```css
/* ✅ components/button.css — ถูกต้อง */
background: var(--brand);        /* Foundation → Components */

/* ✅ features/cashier/cashier.css — ถูกต้อง */
@import "components/card.css";   /* Components → Features */
```

ตัวอย่างที่ผิด

```css
/* ❌ components/card.css — ผิด */
.table-tile { ... }  /* Feature selector ใน Component layer */
```

---

## 5. Import Order — ลำดับ Import

`app.css` import ตามลำดับนี้ทุกประการ

```css
/* 1. FOUNDATION — รากฐาน */
@import "foundation/variables.css";     /* Design tokens */
@import "foundation/reset.css";         /* Base reset */
@import "foundation/typography.css";    /* Typography */

/* 2. COMPONENTS — Reusable UI */
@import "components/button.css";        /* Buttons */
@import "components/card.css";          /* Cards */
@import "components/badge.css";         /* Badges */

/* 3. LAYOUT — Application shell */
@import "layout/app-shell.css";         /* Shell layout */

/* 4. FEATURES — Business UI */
@import "features/cashier/cashier.css";
@import "features/kitchen/kitchen.css";
@import "features/dashboard/dashboard.css";
@import "features/reports/reports.css";
@import "features/settings/settings.css";
```

**เหตุผลที่ import order มีความสำคัญ**

1. **Cascade** — CSS ก่อนหน้ามี specificity ต่ำกว่า ถูก override โดย CSS ทีหลัง
2. **Predictability** — Foundation → Components → Layout → Features ทำให้คาดเดาได้ว่า selector ไหนชนะ
3. **Debugging** — ถ้ารู้ว่า CSS ทำงานจากบนลงล่าง จะหา source ของปัญหาได้ง่ายกว่า

---

## 6. Feature Ownership — ใครเป็นเจ้าของอะไร

### Cashier

| Selector | ไฟล์ |
|----------|------|
| `.cashier-layout` | `features/cashier/cashier.css` |
| `.table-grid-redesign` | `features/cashier/cashier.css` |
| `.table-tile` | `features/cashier/cashier.css` |
| `.table-map, .order-builder` | `features/cashier/cashier.css` |
| `.menu-chips` | `features/cashier/cashier.css` |
| `.order-lines`, `.order-line`, `.rank-row` | `features/cashier/cashier.css` |
| `.bill-dock` | `features/cashier/cashier.css` |
| `.empty-state` | `features/cashier/cashier.css` |

### Kitchen

| Selector | ไฟล์ |
|----------|------|
| `.kanban-grid` | `features/kitchen/kitchen.css` |
| `.ticket-card` | `features/kitchen/kitchen.css` |
| `.ticket-top` | `features/kitchen/kitchen.css` |
| `.ticket-action` | `features/kitchen/kitchen.css` |

### Dashboard

| Selector | ไฟล์ |
|----------|------|
| `.pos-live-dot` | `features/dashboard/dashboard.css` |
| `.hero-copy` | `features/dashboard/dashboard.css` |

### Reports

| Selector | ไฟล์ |
|----------|------|
| `.report-canvas` | `features/reports/reports.css` |
| `.report-tabs` | `features/reports/reports.css` |

### Settings

| Selector | ไฟล์ |
|----------|------|
| `.settings-form` | `features/settings/settings.css` |
| `.toggle-row` | `features/settings/settings.css` |
| `.range-readout` | `features/settings/settings.css` |

**กฎ**: Feature หนึ่งเป็นเจ้าของ Business UI ของตัวเองเท่านั้น ถ้า selector ถูกใช้โดยหลาย feature แสดงว่ามันควรเป็น Component ไม่ใช่ Feature

---

## 7. Component Ownership — Components ที่ใช้ร่วมกัน

Components คือ UI ที่ **ไม่ได้ belong กับ feature ใด feature หนึ่ง**

### Button (`components/button.css`)

| Selector | ใช้โดย |
|----------|--------|
| `.primary-action`, `.secondary-action` | ทุก feature |
| `.icon-button`, `.profile-button` | ทุก feature |
| `.mini-button` | ทุก feature |

### Card (`components/card.css`)

| Selector | ใช้โดย |
|----------|--------|
| `.surface-panel` | Dashboard, Reports, Settings |
| `.metric-tile` | Dashboard |
| `.launch-card` | Dashboard |
| `.queue-column` | Kitchen |

### Badge (`components/badge.css`)

| Selector | ใช้โดย |
|----------|--------|
| `.nav-badge` | ทุกหน้า (sidebar) |
| `.state-pill` | Cashier, Kitchen |

**เหตุผลที่ components ต้องแยกจาก features**

1. **Reusability** — ถ้าอยู่ใน feature จะ import ยาก
2. **Maintainability** — แก้ครั้งเดียว affects ทุกที่ที่ใช้
3. **Testability** — Component ที่แยกออกมาทดสอบได้ง่ายกว่า

---

## 8. Cross-Feature Rule — กฎการข้าม Feature

### ปัญหา

สมมติว่า `cashier.css`, `reports.css`, และ `settings.css` ต้องการ selector
`.table-map`, `.order-builder`, `.report-canvas`, `.settings-form { padding: 16px; }`

**วิธีที่ผิด**
```css
/* ❌ — Cross-feature coupling */
.table-map, .order-builder, .report-canvas, .settings-form { padding: 16px; }
```
ปัญหาคือ ownership ไม่ชัดเจน —  selector นี้อยู่ใน feature ไหน?

**วิธีที่ถูกต้อง**
```css
/* ✅ — Duplication เล็กน้อย ดีกว่า Coupling */

/* features/cashier/cashier.css */
.table-map, .order-builder { padding: 16px; }

/* features/reports/reports.css */
.report-canvas { padding: 16px; }

/* features/settings/settings.css */
.settings-form { padding: 16px; }
```

### กฎ

- **Duplication เล็กน้อย ดีกว่า Coupling ข้าม Feature**
- ถ้า selector 3 บรรทัดซ้ำกันใน 2 features → ยอม
- ถ้า selector 30 บรรทัดซ้ำกันใน 2 features → นั่นคือ Component — ย้ายไป components/

---

## 9. Responsive Rule — การจัดการ Media Queries

### สถานะปัจจุบัน

Media queries ทั้งหมดยังอยู่ใน `app.css`

```css
/* app.css */
@media (max-width: 980px) {
    .metric-strip { grid-template-columns: repeat(2, minmax(0, 1fr)); }
    .workspace-grid, .dashboard-panels, .settings-grid { grid-template-columns: 1fr 1fr; }
    .cashier-layout { grid-template-columns: 1fr; }
    .kanban-grid { grid-template-columns: repeat(3, 320px); overflow-x: auto; }
}
```

### กฎในอนาคต

**Responsive rules ต้อง belong กับ owner เดียวกับ base selector**

ตัวอย่าง

```css
/* ✅ — ถูกต้อง */
/* features/kitchen/kitchen.css */
.kanban-grid {
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
    gap: 14px;
    min-height: calc(100vh - 150px);
}

@media (max-width: 980px) {
    .kanban-grid { grid-template-columns: repeat(3, 320px); overflow-x: auto; }
}
```

---

## 10. Adding New CSS — วิธีเพิ่ม CSS ใหม่

### Decision Flow

```
คุณกำลังจะเพิ่ม CSS ใหม่
│
├─ เป็น reusable UI หรือไม่? (ใช้หลายหน้า)
│  ├─ ใช่ → ไปที่ Components
│  └─ ไม่ → ไปต่อ
│
├─ เป็น page-specific หรือไม่?
│  ├─ ใช่ → ไปที่ Feature
│  └─ ไม่ → ไปต่อ
│
├─ เป็น application layout หรือไม่?
│  ├─ ใช่ → Layout
│  └─ ไม่ → ไปต่อ
│
└─ เป็น foundation หรือไม่? (variables, reset, typography)
   ├─ ใช่ → Foundation
   └─ ไม่ → ทบทวนว่า selector นี้จำเป็นจริงหรือ
```

### ตัวอย่าง

**เพิ่มปุ่มใหม่**

ถาม: ใช้กี่หน้า?
- 1 หน้า → Feature CSS (ของ feature นั้น)
- 2+ หน้า → `components/button.css`

**เพิ่ม feature ใหม่ (เช่น Reports)**

1. สร้าง `features/reports/reports.css`
2. เพิ่ม `@import "features/reports/reports.css"` ใน `app.css`
3. ถ้ามี sub-components → สร้างแยกไฟล์ใน `features/reports/`

**เพิ่ม layout ใหม่**

1. สร้าง `layout/xxx.css`
2. ใส่เฉพาะ layout properties (display, grid, flex, padding, margin)
3. ห้ามใส่ colors, borders, shadows

**เพิ่ม typography ใหม่**

1. ใส่ใน `foundation/typography.css`
2. ถ้าเป็น typography ล้วน ๆ (font-size, font-weight, line-height)
3. ถ้ามี layout properties ผสม → เก็บไว้ใน Feature/Component

---

## 11. Naming Convention — ระบบการตั้งชื่อ

### หลักการ

1. **ใช้ kebab-case** — ตัวพิมพ์เล็กทั้งหมด คั่นด้วย dash
   - ✅ `.table-tile`, `.order-line`, `.settings-form`
   - ❌ `.tableTile`, `.orderLine`, `.settingsForm`

2. **ใช้ prefix** — บอกที่มาของ selector
   - `.pos-*` → POS Shell component (เช่น `.pos-shell`, `.pos-rail`, `.pos-nav-link`)
   - อนาคตอาจมี `.cashier-*`, `.kitchen-*`

3. **หลีกเลี่ยง generic names**
   - ✅ `.primary-action`, `.ticket-card`, `.bill-dock`
   - ❌ `.button`, `.card`, `.panel`, `.container`

4. **BEM-like สำหรับ child elements**
   - `.ticket-card` → `.ticket-top`, `.ticket-action`
   - `.metric-tile` → `.metric-tile span`, `.metric-tile strong`

---

## 12. Migration Summary — สรุปการย้าย

### Sprint B — Design Tokens

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `foundation/variables.css` | 19 CSS custom properties |

### Sprint C — Reset

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `foundation/reset.css` | `*`, `html, body`, `body`, `button, input, select` |

### Sprint D — Typography

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `foundation/typography.css` | `h1`, `h2`, `.pos-title`, `.pos-nav-link span` |

### Sprint E — Button Component

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `components/button.css` | 8 reusable button selectors |

### Sprint F — Card Component

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `components/card.css` | 10 reusable card selectors |

### Sprint G — Layout

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `layout/app-shell.css` | 8 pure layout selectors |

### Sprint H — Badge Component

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `components/badge.css` | 4 reusable badge selectors |

### Sprint I — Cashier Feature

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `features/cashier/cashier.css` | 14 cashier selectors |

### Sprint J — Kitchen Feature

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `features/kitchen/kitchen.css` | 5 kitchen selectors |

### Sprint K — Dashboard Feature

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `features/dashboard/dashboard.css` | 2 dashboard selectors |

### Sprint L — Reports Feature

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `features/reports/reports.css` | 2 reports selectors |

### Sprint M — Settings Feature

| จาก | ไป | อะไร |
|-----|-----|------|
| `app.css` | `features/settings/settings.css` | 7 settings selectors |

### ผลลัพธ์

| Metric | ก่อน | หลัง |
|--------|------|------|
| `app.css` selectors | ~565 บรรทัด | 12 `@import` + 3 media queries |
| ไฟล์ CSS ทั้งหมด | 1 ไฟล์ | 16 ไฟล์ |
| Build errors | — | **0 errors ตลอดทุก Sprint** |
| Visual regressions | — | **0 (UI identical)** |

---

## 13. Future Work — งานที่เหลือในอนาคต

### Responsible Ownership ของ Media Queries

Media queries ทั้งหมดยังอยู่ใน `app.css` งานในอนาคตคือย้ายไปยังไฟล์ของ owner

```css
/* app.css → features/kitchen/kitchen.css */
.kanban-grid { ... }
@media (max-width: 980px) { .kanban-grid { ... } }
```

### CSS Linting

เพิ่ม Stylelint เพื่อ enforce กฎ

- `stylelint-config-standard`
- custom rule ห้าม import ข้าม layer
- custom rule บังคับ BEM-like naming

### Design Tokens Expansion

เพิ่ม tokens ที่จำเป็น

- `--font-mono` สำหรับ monospace
- `--font-size-*` scale ที่สมบูรณ์
- `--spacing-*` scale
- `--z-*` สำหรับ z-index management

### Dark Theme

- เพิ่ม `[data-theme="dark"]` variants
- ใช้ CSS custom properties เพื่อสลับ theme
- Dark theme variable ควรแยกไฟล์ `foundation/dark.css`

### Animation Layer

- `foundation/animation.css` มีโครงสร้างเตรียมไว้แล้ว
- กำหนด `--ease-*`, `--duration-*` tokens
- Utility classes สำหรับ animation ทั่วไป

### Remaining Selectors ใน app.css

- `.pos-rail`, `.pos-brand`, `.brand-mark`, `.brand-copy`
- `.pos-nav-link`, `.nav-icon`
- `.pos-topbar`, `.pos-kicker, .eyebrow`, `.pos-topbar-actions`
- `.segmented-control` (+ children)
- `.toast-card`
- `.queue-column`
- `h1, h2 { margin: 0; }`
- 3 Media query blocks

---

## 14. Conclusion — สรุป

CSS Architecture ของ JLek POS ออกแบบมาเพื่อ

```
Maintainability > Convenience
Feature Ownership > Coupling
Duplication > Cross-Feature Dependencies
```

**หลักการสำคัญ**

1. **ทุก CSS มีบ้าน** — selector ทุกตัวรู้ว่า belong กับ layer ไหน
2. **Dependency goes one way** — Foundation → Components → Layout → Features
3. **No cross-feature coupling** — ถ้าต้องซ้ำ ก็ยอม
4. **app.css เป็นแค่ index** — ไม่มี selector ใน `app.css` (ยกเว้น media queries ที่ยังรอ迁移)
5. **Build ต้องผ่าน** — ถ้าเพิ่ม CSS แล้ว build fail = มีปัญหา

เอกสารนี้เป็นส่วนหนึ่งของ AI Context สำหรับนักพัฒนา

---

*อัปเดตล่าสุด: 19 กรกฎาคม 2026*