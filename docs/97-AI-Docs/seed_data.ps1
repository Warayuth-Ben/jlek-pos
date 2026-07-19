# JLek POS - Development Seed Data (Fixed for actual API contract)
$api = "http://localhost:5287"

Write-Host "=== JLek POS Development Seed Data ===" -ForegroundColor Cyan

# Categories - expects ?name= query parameter
Write-Host "Creating Categories..." -ForegroundColor Yellow
$cat1 = curl.exe -s -X POST "$api/categories?name=Main+Course" | ConvertFrom-Json
$catM = $cat1.id
Write-Host "  Main Course: $catM" -ForegroundColor Green

$cat2 = curl.exe -s -X POST "$api/categories?name=Drinks" | ConvertFrom-Json
$catD = $cat2.id
Write-Host "  Drinks: $catD" -ForegroundColor Green

# Products - expects JSON body with name and categoryId
Write-Host "Creating Products..." -ForegroundColor Yellow

$body1 = "{`"name`":`"Chicken Rice`",`"categoryId`":`"$catM`"}"
$p1 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body1 | ConvertFrom-Json
curl.exe -s -X POST "$api/products/$($p1.id)/suggested-prices?amount=45" -H "Content-Type: application/json" -d "" | Out-Null
Write-Host "  Chicken Rice (45 THB): $($p1.id)" -ForegroundColor Green

$body2 = "{`"name`":`"Special Chicken Rice`",`"categoryId`":`"$catM`"}"
$p2 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body2 | ConvertFrom-Json
curl.exe -s -X POST "$api/products/$($p2.id)/suggested-prices?amount=55" -H "Content-Type: application/json" -d "" | Out-Null
Write-Host "  Special Chicken Rice (55 THB): $($p2.id)" -ForegroundColor Green

$body3 = "{`"name`":`"Fried Chicken`",`"categoryId`":`"$catM`"}"
$p3 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body3 | ConvertFrom-Json
curl.exe -s -X POST "$api/products/$($p3.id)/suggested-prices?amount=35" -H "Content-Type: application/json" -d "" | Out-Null
Write-Host "  Fried Chicken (35 THB): $($p3.id)" -ForegroundColor Green

$body4 = "{`"name`":`"Drinking Water`",`"categoryId`":`"$catD`"}"
$p4 = curl.exe -s -X POST "$api/products" -H "Content-Type: application/json" -d $body4 | ConvertFrom-Json
curl.exe -s -X POST "$api/products/$($p4.id)/suggested-prices?amount=10" -H "Content-Type: application/json" -d "" | Out-Null
Write-Host "  Drinking Water (10 THB): $($p4.id)" -ForegroundColor Green

# Tables - expects ?name= query parameter
Write-Host "Creating Tables..." -ForegroundColor Yellow
$t1 = curl.exe -s -X POST "$api/tables?name=T1" | ConvertFrom-Json
$tid1 = $t1.id
$t2 = curl.exe -s -X POST "$api/tables?name=T2" | ConvertFrom-Json
$tid2 = $t2.id
$t3 = curl.exe -s -X POST "$api/tables?name=T3" | ConvertFrom-Json
$tid3 = $t3.id
$t4 = curl.exe -s -X POST "$api/tables?name=T4" | ConvertFrom-Json
$tid4 = $t4.id
Write-Host "  T1: $tid1, T2: $tid2, T3: $tid3, T4: $tid4" -ForegroundColor Green

# Verify
Write-Host "`nVERIFICATION:" -ForegroundColor Cyan
$cats = curl.exe -s "$api/categories" | ConvertFrom-Json
Write-Host "  Categories: $($cats.Count)" -ForegroundColor Green
$prods = curl.exe -s "$api/products" | ConvertFrom-Json
Write-Host "  Products: $($prods.Count)" -ForegroundColor Green
$tables = curl.exe -s "$api/tables" | ConvertFrom-Json
Write-Host "  Tables: $($tables.Count)" -ForegroundColor Green

Write-Host "`nIDs for workflow:" -ForegroundColor Cyan
Write-Host "T1=$tid1 T2=$tid2 P1=$($p1.id) P2=$($p2.id) P3=$($p3.id) P4=$($p4.id)"