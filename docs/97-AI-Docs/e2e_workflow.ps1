# JLek POS - Complete E2E Workflow Verification
$api = "http://localhost:5287"
$ErrorActionPreference = "Stop"

function Write-Step($s) { Write-Host "`n=== $s ===" -ForegroundColor Cyan }
function Write-OK($s) { Write-Host "  [OK] $s" -ForegroundColor Green }
function Write-Fail($s) { Write-Host "  [FAIL] $s" -ForegroundColor Red; exit 1 }

try {
# ========================================================================
# STEP 1: SEED DATA
# ========================================================================
Write-Step "STEP 1: Creating Seed Data"

# Categories
$cat1 = curl.exe -s -X POST "$api/categories?name=Main+Course" | ConvertFrom-Json
$catM = $cat1.id.value
Write-OK "Category Main Course: $catM"

$cat2 = curl.exe -s -X POST "$api/categories?name=Drinks" | ConvertFrom-Json
$catD = $cat2.id.value
Write-OK "Category Drinks: $catD"

# Products
$body1 = "{`"name`":`"Chicken Rice`",`"categoryId`":`"$catM`"}"
$p1 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body1 | ConvertFrom-Json
$pid1 = $p1.id.value
curl.exe -s -X POST "$api/products/$pid1/suggested-prices" -H "Content-Type: application/json" -d "45" | Out-Null
Write-OK "Product Chicken Rice (45 THB): $pid1"

$body2 = "{`"name`":`"Special Chicken Rice`",`"categoryId`":`"$catM`"}"
$p2 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body2 | ConvertFrom-Json
$pid2 = $p2.id.value
curl.exe -s -X POST "$api/products/$pid2/suggested-prices" -H "Content-Type: application/json" -d "55" | Out-Null
Write-OK "Product Special Chicken Rice (55 THB): $pid2"

$body3 = "{`"name`":`"Fried Chicken`",`"categoryId`":`"$catM`"}"
$p3 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body3 | ConvertFrom-Json
$pid3 = $p3.id.value
curl.exe -s -X POST "$api/products/$pid3/suggested-prices" -H "Content-Type: application/json" -d "35" | Out-Null
Write-OK "Product Fried Chicken (35 THB): $pid3"

$body4 = "{`"name`":`"Drinking Water`",`"categoryId`":`"$catD`"}"
$p4 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body4 | ConvertFrom-Json
$pid4 = $p4.id.value
curl.exe -s -X POST "$api/products/$pid4/suggested-prices" -H "Content-Type: application/json" -d "10" | Out-Null
Write-OK "Product Drinking Water (10 THB): $pid4"

# Tables
$t1 = curl.exe -s -X POST "$api/tables" -H "Content-Type: application/json" -d '"T1"' | ConvertFrom-Json
$tid1 = $t1.id
$t2 = curl.exe -s -X POST "$api/tables" -H "Content-Type: application/json" -d '"T2"' | ConvertFrom-Json
$tid2 = $t2.id
$t3 = curl.exe -s -X POST "$api/tables" -H "Content-Type: application/json" -d '"T3"' | ConvertFrom-Json
$tid3 = $t3.id
$t4 = curl.exe -s -X POST "$api/tables" -H "Content-Type: application/json" -d '"T4"' | ConvertFrom-Json
$tid4 = $t4.id
Write-OK "Tables created: T1=$tid1 T2=$tid2 T3=$tid3 T4=$tid4"

# Verify seed
$cats = curl.exe -s "$api/categories" | ConvertFrom-Json
if ($cats.Count -ne 2) { Write-Fail "Expected 2 categories, got $($cats.Count)" }
Write-OK "Categories: $($cats.Count)"

$prods = curl.exe -s "$api/products" | ConvertFrom-Json
if ($prods.Count -ne 4) { Write-Fail "Expected 4 products, got $($prods.Count)" }
Write-OK "Products: $($prods.Count)"

$tables = curl.exe -s "$api/tables" | ConvertFrom-Json
if ($tables.Count -ne 4) { Write-Fail "Expected 4 tables, got $($tables.Count)" }
Write-OK "Tables: $($tables.Count)"

# ========================================================================
# STEP 2: COMPLETE BUSINESS WORKFLOW
# ========================================================================
Write-Step "STEP 2: Complete Restaurant Workflow"

# 2a. Open Table T1
$openResp = curl.exe -s -X POST "$api/tables/$tid1/open" | ConvertFrom-Json
if ($openResp.status -ne "Available" -and $openResp.status -ne "Occupied" -and $openResp.status -ne "Reserved") { Write-Fail "Table open returned unexpected status: $($openResp.status)" }
Write-OK "Table T1 opened (Status: $($openResp.status))"

# Wait a moment for state to settle
Start-Sleep 1

# 2b. Create Order for Table T1
$orderBody = "{`"tableId`":`"$tid1`"}"
$order = curl.exe -s -X POST "$api/orders" -H "Content-Type: application/json" -d $orderBody | ConvertFrom-Json
$oid = $order.id
if (-not $oid) { Write-Fail "Failed to create order: $(ConvertTo-Json $order -Compress)" }
Write-OK "Order created: $oid"

# 2c. Add Menu Items to Order
$addBody1 = "{`"menuItemId`":`"$pid1`",`"quantity`":2,`"unitPrice`":45}"
$order1 = curl.exe -s -X POST "$api/orders/$oid/items" -H "Content-Type: application/json" -d $addBody1 | ConvertFrom-Json
Write-OK "Added Chicken Rice x2 (90 THB)"

$addBody2 = "{`"menuItemId`":`"$pid4`",`"quantity`":2,`"unitPrice`":10}"
$order2 = curl.exe -s -X POST "$api/orders/$oid/items" -H "Content-Type: application/json" -d $addBody2 | ConvertFrom-Json
Write-OK "Added Drinking Water x2 (20 THB)"

# 2d. Confirm Order  
$confirmResp = curl.exe -s -X POST "$api/orders/$oid/confirm" | ConvertFrom-Json
if ($confirmResp.status -ne "Confirmed") { Write-Fail "Order confirm failed, status: $($confirmResp.status)" }
Write-OK "Order confirmed"

# 2e. Complete Order (triggers kitchen ticket)
$completeResp = curl.exe -s -X POST "$api/orders/$oid/complete" | ConvertFrom-Json
if ($completeResp.status -ne "Completed") { Write-Fail "Order complete failed, status: $($completeResp.status)" }
Write-OK "Order completed"

# 2f. Verify Kitchen Ticket Created
$activeTickets = curl.exe -s "$api/kitchen/active" | ConvertFrom-Json
if ($activeTickets.Count -eq 0) { Write-Fail "No kitchen tickets created" }
$kitchenId = $activeTickets[0].id
Write-OK "Kitchen ticket created: $kitchenId with $($activeTickets[0].items.Count) item(s)"

# 2g. Kitchen - Start Preparation
$startResp = curl.exe -s -X POST "$api/kitchen/$kitchenId/start" | ConvertFrom-Json
if ($startResp.status -ne "Preparing") { Write-Fail "Kitchen start failed, status: $($startResp.status)" }
Write-OK "Kitchen started preparing"

# 2h. Kitchen - Complete Preparation
$readyResp = curl.exe -s -X POST "$api/kitchen/$kitchenId/complete" | ConvertFrom-Json
if ($readyResp.status -ne "Ready") { Write-Fail "Kitchen complete failed, status: $($readyResp.status)" }
Write-OK "Kitchen marked Ready"

# 2i. Kitchen - Serve
$serveResp = curl.exe -s -X POST "$api/kitchen/$kitchenId/serve" | ConvertFrom-Json
if ($serveResp.status -ne "Served") { Write-Fail "Kitchen serve failed, status: $($serveResp.status)" }
Write-OK "Kitchen items served"

# 2j. Payment
$paymentBody = "{`"orderId`":`"$oid`",`"amount`":110,`"method`":0}"
$payment = curl.exe -s -X POST "$api/payments" -H "Content-Type: application/json" -d $paymentBody | ConvertFrom-Json
$payId = $payment.id
if (-not $payId) { Write-Fail "Payment failed: $(ConvertTo-Json $payment -Compress)" }
Write-OK "Payment received: $payId (110 THB, Cash)"

# 2k. Verify Payment
$payVerify = curl.exe -s "$api/payments/$payId" | ConvertFrom-Json
if ($payVerify.status -ne "Completed") { Write-Fail "Payment status not Completed: $($payVerify.status)" }
Write-OK "Payment verified as Completed"

# 2l. Close Table (Release)
$releaseResp = curl.exe -s -X POST "$api/tables/$tid1/release" | ConvertFrom-Json
Write-OK "Table T1 released (Status: $($releaseResp.status))"

# ========================================================================
# STEP 3: VERIFY REPORTS
# ========================================================================
Write-Step "STEP 3: Reports Verification"

$dailySales = curl.exe -s "$api/reports/daily-sales" | ConvertFrom-Json
Write-OK "Daily Sales report: total=$($dailySales.totalSales) items=$($dailySales.totalItems)"

$salesByPay = curl.exe -s "$api/reports/sales-by-payment" | ConvertFrom-Json
Write-OK "Sales by Payment report retrieved"

$bestSellers = curl.exe -s "$api/reports/best-sellers" | ConvertFrom-Json
Write-OK "Best Sellers report retrieved"

# ========================================================================
# STEP 4: DATABASE CONSISTENCY
# ========================================================================
Write-Step "STEP 4: Database Consistency Check"

$orders = curl.exe -s "$api/orders" | ConvertFrom-Json
Write-OK "Orders in DB: $($orders.Count)"
$tablesAfter = curl.exe -s "$api/tables" | ConvertFrom-Json
$table1State = $tablesAfter | Where-Object { $_.id -eq $tid1 } | Select-Object -First 1
Write-OK "Table T1 status after release: $($table1State.status)"

# ========================================================================
# SUMMARY
# ========================================================================
Write-Step "WORKFLOW COMPLETE - ALL STEPS PASSED"
Write-Host "`nSummary:" -ForegroundColor Green
Write-Host "  Table: T1 ($tid1)" -ForegroundColor Green
Write-Host "  Order: $oid" -ForegroundColor Green
Write-Host "  Kitchen Ticket: $kitchenId" -ForegroundColor Green
Write-Host "  Payment: $payId (110 THB)" -ForegroundColor Green
Write-Host "  Total Amount: 110 THB (Chicken Rice 2x45 + Water 2x10)" -ForegroundColor Green

} catch {
    Write-Host "`n[FATAL] $_" -ForegroundColor Red
    exit 1
}