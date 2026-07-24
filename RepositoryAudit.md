# RepositoryAudit.md

## JLek POS — Frontend Repository Audit

**Task**: ETS-000 Repository Audit & Gap Analysis  
**Date**: 2026-07-24  
**Auditor**: Senior Frontend Developer (AI)

---

## Executive Summary

| Category | Score |
|----------|-------|
| **Overall** | 7/10 — Good foundation, needs refinement |
| **Architecture** | 8/10 — Clean Architecture, Presentation layer present |
| **UI Foundation** | 7/10 — Components exist but lack ETS-specified parameters |
| **Component Library** | 7/10 — 13 shared components, some incomplete per ETS spec |
| **Theme** | 6/10 — All CSS variables in one file, not separated per ETS |
| **Responsive** | 7/10 — CSS utilities present, needs verification |
| **Reuse Opportunity** | 8/10 — High reuse possible with minor improvements |
| **Technical Debt** | 4/10 — Low; some components need parameter alignment |

---

## Existing Components

| Component | File | Status | Action | Notes |
|-----------|------|--------|--------|-------|
| `PageHeader` | `Components/Shared/PageHeader.razor` | ✅ Exists | **Improve** | Has Title, Kicker, Actions. Missing Subtitle per ETS. Rename Kicker→Subtitle or add Subtitle parameter. |
| `PanelHeader` | `Components/Shared/PanelHeader.razor` | ✅ Exists | **Reuse** | ETS requires `Panel` (not `PanelHeader`). Need `Panel.razor` with Header + ChildContent. |
| `Card` | `Components/Shared/Card.razor` | ✅ Exists | **Improve** | Simple Class+ChildContent wrapper. ETS requires generic reusable container — current implementation is acceptable but could add Header parameter. |
| `Badge` | `Components/Shared/Badge.razor` | ✅ Exists | **Improve** | Only supports "occupied"/"available" variants. ETS requires `StatusBadge` with Success, Warning, Danger, Info, Neutral. Should be renamed/refactored to support all 6 variants. |
| `EmptyState` | `Components/Shared/EmptyState.razor` | ✅ Exists | **Improve** | Has ChildContent only. ETS requires Title + Message parameters. |
| `LoadingSpinner` | `Components/Shared/LoadingSpinner.razor` | ✅ Exists | **Reuse** | Has Message parameter. ETS requires "Loading" placeholder — this is sufficient. Rename folder to `Loading/` if folder-based structure required. |
| `Button` | `Components/Shared/Button.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `Divider` | `Components/Shared/Divider.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `SegmentedControl` | `Components/Shared/SegmentedControl.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `SearchBox` | `Components/Shared/SearchBox.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `ToastNotification` | `Components/Shared/ToastNotification.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `ConfirmDialog` | `Components/Shared/ConfirmDialog.razor` | ✅ Exists | **Reuse** | Not in ETS scope. Keep as-is. |
| `ToastType` | `Components/Shared/ToastType.cs` | ✅ Exists | **Reuse** | Enum for toast types. Keep as-is. |

---

## Missing Components (per ETS spec)

| Component | Reason | Priority | Notes |
|-----------|--------|----------|-------|
| `StatusBadge` | ETS requires dedicated StatusBadge with 6 variants (Success, Warning, Danger, Info, Neutral, custom) | **High** | Current Badge only has 2 variants. Should create `StatusBadge.razor` with proper variant enum and CSS. |
| `Panel` | ETS requires Panel with Header + ChildContent | **Medium** | `PanelHeader.razor` exists but is not a container. Need Panel.razor. |
| `Loading` | ETS requires simple loading placeholder | **Low** | `LoadingSpinner.razor` already satisfies this. Folder rename only if needed. |
| `EmptyState` (improved) | ETS requires Title + Message parameters | **Medium** | Current EmptyState has only ChildContent. Add Title/Message. |

---

## Theme Audit

| Area | Status | Details |
|------|--------|---------|
| **CSS Variables** | ✅ Exists | `wwwroot/css/foundation/variables.css` — single file with all tokens |
| **Colors** | ⚠️ Partial | Has --primary(--brand), --secondary, --surface, --background, --success(--available), --warning(--occupied), --danger. Missing --info. |
| **Typography** | ❌ Not separated | Font family defined in variables.css via --font. No separate typography.css with headings/body scales. |
| **Spacing** | ❌ Not separated | No --space-xs, --space-sm, --space-md, --space-lg variables found. Spacing is handled via utility classes in `utilities.css`. |
| **Radius** | ❌ Not separated | Only `--radius: 8px` in variables.css. No --radius-sm, --radius-md, --radius-lg. |
| **Dark Theme** | ❌ Not present | No dark mode variables or media queries. |
| **Light Theme** | ✅ Present | Default theme is light. |

### ETS-required CSS variables check

| ETS Variable | Status | Notes |
|-------------|--------|-------|
| `--primary` | ✅ Exists | Named `--brand` in current codebase |
| `--secondary` | ⚠️ Missing | Not explicitly named "secondary" |
| `--surface` | ✅ Exists | `--surface`, `--surface-2` |
| `--background` | ✅ Exists | `--bg` |
| `--success` | ⚠️ Partial | Named `--available` |
| `--warning` | ⚠️ Partial | Named `--occupied` |
| `--danger` | ✅ Exists | `--danger` |
| `--info` | ❌ Missing | Not defined |
| `--space-xs` | ❌ Missing | Uses utility classes instead |
| `--space-sm` | ❌ Missing | Uses utility classes instead |
| `--space-md` | ❌ Missing | Uses utility classes instead |
| `--space-lg` | ❌ Missing | Uses utility classes instead |
| `--radius-sm` | ❌ Missing | Only `--radius: 8px` |
| `--radius-md` | ❌ Missing | Only `--radius: 8px` |
| `--radius-lg` | ❌ Missing | Only `--radius: 8px` |

---

## Layout Audit

| Area | Status | Notes |
|------|--------|-------|
| **MainLayout** | ✅ Exists | `Layout/MainLayout.razor` + `MainLayout.razor.css` |
| **Navigation** | ✅ Exists | `Layout/NavMenu.razor` + `NavMenu.razor.css` |
| **Workspace Shell** | ✅ Exists | `Presentation/Components/WorkspaceShell.razor` |
| **Responsive** | ✅ Verified | `wwwroot/css/foundation/utilities.css` has responsive utilities |

---

## CSS Audit

| Area | Status | Notes |
|------|--------|-------|
| **CSS Isolation** | ✅ Used | `MainLayout.razor.css`, `NavMenu.razor.css` use scoped CSS |
| **CSS Variables** | ✅ Used | Single `variables.css` with design tokens |
| **Hardcoded Colors** | ⚠️ Detected | Some component `.razor.css` files may have hardcoded colors. Need per-file verification. |
| **Duplicate CSS** | ⚠️ Potential | `app.css` imports all CSS files. Some overlap between foundation and component CSS. |
| **CSS Architecture** | ✅ Verified | 16-file CSS architecture per docs/97-AI-Docs/101-css-architecture.md |

---

## Architecture Audit

| Area | Status | Notes |
|------|--------|-------|
| **Folder Structure** | ✅ Clean | Pages/, Components/ (feature-based), Presentation/, Layout/, Contracts/, Clients/ |
| **Naming** | ✅ Consistent | PascalCase for components, kebab-case for CSS files |
| **Namespaces** | ✅ Consistent | `JLek.POS.Web.*` namespace matches folder structure |
| **Dependency Direction** | ✅ Clean | Components receive state via parameters, no direct API calls from shared components |
| **Presentation Layer** | ✅ Exists | `Presentation/` folder with Platform, Context, Components sub-folders |
| **Presentation/Theme/** | ❌ Missing | ETS requires Theme, Styles, Icons, Constants sub-folders |

---

## Risks

| # | Risk | Severity | Mitigation |
|---|------|----------|------------|
| 1 | Project Status shows UI Foundation 100% but ETS requires different parameter names/shapes | **Medium** | Minor parameter additions (Subtitle, Title+Message) won't break existing usage |
| 2 | EmptyState has only ChildContent; adding Title/Message might break existing usages if ChildContent order changes | **Low** | Keep ChildContent as optional, add Title/Message as additional parameters |
| 3 | Badge.razor supports only 2 variants; ETS requires 6 | **Low** | Create StatusBadge.razor alongside existing Badge.razor (don't modify existing to avoid breaking KitchenStatusBadge usage) |
| 4 | CSS variables not separated per ETS spec but already structurally sound | **Low** | Can defer separation; current single-file approach works at runtime |

---

## Recommendations

### Immediate (Sprint 0 — Current Task)

1. **Create `StatusBadge.razor`** — New component with 6 variants: Success, Warning, Danger, Info, Neutral, custom
   - Do NOT modify existing `Badge.razor` (used by other parts of the app)
2. **Improve `EmptyState.razor`** — Add Title (string) and Message (string) parameters while keeping ChildContent optional
3. **Create `Panel.razor`** — Container with Header (string/RenderFragment) + ChildContent
4. **Improve `PageHeader.razor`** — Add Subtitle parameter (keep Kicker for backward compatibility or rename)

### CSS Improvements (Sprint 0)

5. **Create `wwwroot/css/foundation/Colors.css`** — Extract color tokens from existing variables.css
6. **Create `wwwroot/css/foundation/Spacing.css`** — Add --space-xs, --space-sm, --space-md, --space-lg variables
7. **Create `wwwroot/css/foundation/Typography.css`** — Extract font/typography tokens
8. **Create `wwwroot/css/foundation/Radius.css`** — Add --radius-sm, --radius-md, --radius-lg variables

### Presentation Structure (Sprint 0)

9. **Create `Presentation/Theme/`** — Folder for theme-related code
10. **Create `Presentation/Styles/`** — Folder for shared styles
11. **Create `Presentation/Icons/`** — Folder for icon constants/definitions
12. **Create `Presentation/Constants/`** — Folder for UI constants

### Sprint 1 (Future)

13. Rename `--brand` → `--primary` for ETS compliance (if not breaking existing usage)
14. Add `--info` color variable
15. Add `--info` color variable

---

## Final Recommendation

**B) Minor improvements required.**

### Rationale

1. **Repository is structurally sound** — Clean Architecture, Presentation layer, feature-based components all present
2. **All core components exist** — PageHeader, Card, EmptyState, LoadingSpinner, Badge all exist and work
3. **Only 3 components need minor improvements** — StatusBadge (new), EmptyState (add params), Panel (new folder)
4. **CSS is functional** — variables.css works but lacks ETS-required separation and spacing/radius tokens
5. **No architectural changes needed** — Current folder structure is compatible with ETS; new folders can be added alongside existing ones
6. **Reuse over replace** — Existing components should be extended, not replaced

**Estimated effort**: 1-2 hours for component improvements + 1 hour for CSS separation + 30 min for Presentation folders = **~3 hours total**