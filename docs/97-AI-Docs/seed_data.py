"""JLek POS - Development Seed Data"""
import json, urllib.request, uuid

API = "http://localhost:5287"

def post(path, data=None, content_type=None):
    body = json.dumps(data).encode() if data else b""
    headers = {}
    if content_type:
        headers["Content-Type"] = content_type
    elif data:
        headers["Content-Type"] = "application/json"
    
    url = API + path
    req = urllib.request.Request(url, data=body or None, headers=headers, method="POST")
    try:
        with urllib.request.urlopen(req) as resp:
            return json.loads(resp.read())
    except urllib.error.HTTPError as e:
        print(f"  ERROR {e.code}: {e.read().decode()}")
        return None

def get(path):
    with urllib.request.urlopen(API + path) as resp:
        return json.loads(resp.read())

print("=== JLek POS Development Seed Data ===")
print()

# Categories
print("Creating Categories...")
cat1 = post("/categories?name=Main+Course")
catM = cat1["id"] if cat1 else None
print(f"  Main Course: {catM}")

cat2 = post("/categories?name=Drinks")
catD = cat2["id"] if cat2 else None
print(f"  Drinks: {catD}")

# Products
print("Creating Products...")
if catM:
    for name, price in [("Chicken Rice", 45), ("Special Chicken Rice", 55), ("Fried Chicken", 35)]:
        p = post("/products", {"name": name, "categoryId": catM})
        if p:
            post(f"/products/{p['id']}/suggested-prices?amount={price}")
            print(f"  {name} ({price} THB): {p['id']}")
    
    p4 = post("/products", {"name": "Drinking Water", "categoryId": catD})
    if p4:
        post(f"/products/{p4['id']}/suggested-prices?amount=10")
        print(f"  Drinking Water (10 THB): {p4['id']}")

# Tables
print("Creating Tables...")
tables = {}
for name in ["T1", "T2", "T3", "T4"]:
    t = post(f"/tables?name={name}")
    if t:
        tables[name] = t["id"]
        print(f"  {name}: {t['id']}")

# Verify
print("\nVERIFICATION:")
cats = get("/categories")
print(f"  Categories: {len(cats)}")
prods = get("/products")
print(f"  Products: {len(prods)}")
tbls = get("/tables")
print(f"  Tables: {len(tbls)}")

print("\nIDs for workflow:")
for k, v in tables.items():
    print(f"{k}={v} ", end="")
print()