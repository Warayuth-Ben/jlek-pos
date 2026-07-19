import re

files = [
    'src/JLek.POS.IntegrationTests/Catalog/ProductCategoryTests.cs',
    'src/JLek.POS.IntegrationTests/Catalog/IngredientTests.cs',
    'src/JLek.POS.IntegrationTests/Tables/DiningTableTests.cs',
    'src/JLek.POS.IntegrationTests/Kitchen/KitchenTicketTests.cs'
]

for fpath in files:
    with open(fpath, encoding='utf-8') as f:
        c = f.read()
    original = c

    # Remove .Value suffix (Id.Value -> Id, result.Id.Value -> result.Id)
    c = re.sub(r'(\w[\w.]*)\.Value\b', r'\1', c)

    # Replace .Should().Be(SomeEnum.XXX) -> .Should().Be("XXX")
    c = re.sub(r'\.Should\(\)\.Be\((ProductCategoryStatus|IngredientStatus|TableStatus|KitchenTicketStatus|ProductVisibility)\.(\w+)\)', r'.Should().Be("\2")', c)

    # Replace == TableStatus.XXX -> == "XXX"
    c = re.sub(r'== TableStatus\.(\w+)', r'== "\1"', c)

    if c != original:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(c)
        print(f'FIXED: {fpath}')
    else:
        print(f'OK: {fpath}')