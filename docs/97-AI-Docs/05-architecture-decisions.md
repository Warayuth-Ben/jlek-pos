\# Architecture Decision Records (ADR)



เอกสารนี้ไม่ใช่คู่มือการใช้งาน



แต่เป็น "ประวัติการตัดสินใจ" ของ JLek POS



ทุกครั้งที่มีการเปลี่ยนแปลง Architecture หรือ Business Rule

ให้บันทึกเหตุผลไว้ที่นี่



\---



\# ADR-001



\## Order ต้องเป็น Aggregate Root



Status



Accepted



Reason



Order คือ Transaction หลักของร้านอาหาร



OrderItem ไม่มีสิทธิ์ถูกแก้ไขโดยตรง



ทุกการเปลี่ยนแปลงต้องผ่าน Order เท่านั้น



Consequences



\- ไม่มี Repository ของ OrderItem

\- ห้าม DbContext.OrderItems.Add()

\- ทุกอย่างต้องผ่าน Order.AddItem()



\---



\# ADR-002



\## Confirm แล้ว Order เป็น Immutable



Status



Accepted



Reason



เมื่อกด Confirm



ระบบถือว่า Kitchen เริ่มทำงานแล้ว



Kitchen Printer



Kitchen Display



History



Audit



ทั้งหมดอ้างอิง Order ที่ถูก Confirm



หากแก้ไข Order เดิม



Kitchen จะไม่รู้ว่าอะไรถูกเพิ่ม



Consequences



\- Confirm แล้วห้ามแก้ไข

\- ห้าม AddItem

\- ห้าม RemoveItem

\- ห้าม ChangeQuantity



หากลูกค้าสั่งเพิ่ม



ต้องสร้าง "Order ใหม่" ที่อยู่ใน Session เดียวกัน



ไม่แก้ไข Order เดิม



\---



\# ADR-003



\## Order Session แยกจาก Order



Status



Accepted



Reason



ลูกค้าไม่ได้จ่าย "Order"



ลูกค้าจ่าย "โต๊ะ"



โต๊ะหนึ่ง



อาจมีหลาย Order



เช่น



Order #1



Order #2



Order #3



แต่ทั้งหมดอยู่ใน Session เดียว



Consequences



Kitchen



Billing



Report



ทำงานผ่าน Session



ไม่ใช่ Order เดี่ยว



\---



\# ADR-004



\## Strongly Typed IDs



Status



Accepted



Reason



ป้องกันการใช้ Guid ผิดประเภท



เช่น



OrderId



CustomerId



MenuId



ไม่สามารถสลับกันได้



Consequences



EF Core ต้องใช้ ValueConverter



\---



\# ADR-005



\## Money เป็น Value Object



Status



Accepted



Reason



Money ไม่ใช่ decimal



Money มีความหมายทางธุรกิจ



สามารถเพิ่ม



ลบ



เปรียบเทียบ



ตรวจสอบความถูกต้อง



Consequences



EF Core ใช้ OwnsOne()



\---



\# ADR-006



\## Domain Entity ห้ามส่งออก API



Status



Accepted



Reason



Domain Model



ไม่ใช่ API Contract



หากส่ง Entity ตรง



จะทำให้



API ผูกกับ Domain



Consequences



ใช้ DTO เท่านั้น



\---



\# ADR-007



\## Repository ไม่คืน IQueryable



Status



Accepted



Reason



Infrastructure



ไม่ควรรั่วออกไปยัง Application



Consequences



Application



ไม่รู้จัก EF Core



ไม่รู้จัก LINQ Provider



\---



\# ADR-008



\## Business Rule อยู่ใน Domain



Status



Accepted



Reason



Business Rule



ไม่ควรอยู่ใน



Controller



Handler



Repository



Consequences



ทุก Rule



ต้องอยู่ใน Domain



และถูกเรียกผ่าน Aggregate



\---



\# ADR-009



\## Cline และ AI Assistant



Status



Accepted



Reason



AI สามารถช่วยเขียนโค้ด



แต่ห้ามเปลี่ยน Business Rule



ห้ามเปลี่ยน Aggregate



ห้ามเปลี่ยน State Machine



โดยไม่มีการ Review



Consequences



AI



ใช้สำหรับ



\- Boilerplate

\- Refactor

\- CRUD

\- DTO

\- Mapping



Architecture และ Business ต้อง Review เสมอ



###### \---



ADR-010



"Decision Log"



###### ทุกครั้งที่เราตัดสินใจเรื่องใหญ่ เช่น

###### 

เปลี่ยน SQL Server → PostgreSQL

###### เลือก Blazor

###### เลือก CQRS

###### เปลี่ยน Kitchen Workflow

###### 

###### ให้เพิ่ม ADR ใหม่แทนการแก้ของเก่า

\---



\# Principle



Model the business.



Do not model the database.



Business มาก่อน Technology เสมอ.

