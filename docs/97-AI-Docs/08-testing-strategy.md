\# Testing Strategy



\## Testing Pyramid



Business Rule

&#x20;   ↓

Unit Test



Application

&#x20;   ↓

Handler Test



Infrastructure

&#x20;   ↓

Integration Test



API

&#x20;   ↓

Endpoint Test



UI

&#x20;   ↓

E2E



\---



Business Rule ทุกข้อ



ต้องมี Unit Test



\---



State Machine ทุก Transition



ต้องมี Test



\---



Bug ทุกตัว



ถ้าแก้แล้ว



ต้องมี Regression Test



\---



ห้าม Merge



###### ถ้า Test ใหม่ไม่ผ่าน

