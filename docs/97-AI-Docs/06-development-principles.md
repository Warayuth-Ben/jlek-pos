\# Development Principles



เอกสารนี้เป็นกฎการพัฒนา JLek POS



AI และ Developer ทุกคนต้องปฏิบัติตาม



\---



\# Principle 1



Business First



ห้ามเริ่มจาก Database



ห้ามเริ่มจาก EF



ห้ามเริ่มจาก API



ให้เริ่มจาก



Business Rule



เสมอ



\---



\# Principle 2



Domain คือหัวใจ



หาก Business Rule อยู่ใน



Controller



Handler



Repository



ถือว่าผิด



Business Rule ต้องอยู่ใน Domain



\---



\# Principle 3



Aggregate เท่านั้น



Entity ภายใน Aggregate



ห้ามถูกแก้ไขโดยตรง



ตัวอย่าง



❌



db.OrderItems.Add(...)



✔



order.AddItem(...)



\---



\# Principle 4



API ไม่รู้จัก Domain



API



↓



DTO



↓



Application



↓



Domain



API ห้ามคืน Domain Entity



\---



\# Principle 5



Application ไม่มี Business Rule



Application



มีหน้าที่



Orchestrate



ไม่ใช่



Decide



\---



\# Principle 6



Infrastructure ไม่มี Logic



Infrastructure



มีหน้าที่



Save



Load



Publish



เท่านั้น



\---



\# Principle 7



ทุก Feature เดินตามลำดับ



Business



↓



Domain



↓



Command / Query



↓



Handler



↓



Repository



↓



Endpoint



↓



Test



ห้ามเริ่มจาก Controller



\---



\# Principle 8



อย่า Refactor เพราะ AI คิดว่าสวยกว่า



Refactor



ต้องมีเหตุผล



เช่น



\- อ่านง่ายขึ้น



\- ลด Duplication



\- เพิ่ม Maintainability



ห้าม Refactor



เพราะ



"AI ชอบ"



\---



\# Principle 9



State Machine สำคัญกว่า CRUD



ระบบ POS



ไม่ใช่ CRUD



แต่คือ Workflow



ดังนั้น



ต้องคิด State ก่อนเสมอ



\---



\# Principle 10



Kitchen มาก่อน Database



ทุก Feature



ให้ถามก่อนว่า



Kitchen จะเห็นอะไร



Billing จะเห็นอะไร



Report จะเห็นอะไร



แล้วค่อยออกแบบ Database



\---



\# Principle 11



Model the Business



Do not model the tables.



Do not model the database.



Model the restaurant.



Model the workflow.



