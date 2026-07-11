\# AI Certification



Purpose



ใช้ประเมินว่า AI พร้อมทำงานกับ JLek POS หรือไม่



\---



\## Part A : Knowledge



□ โปรเจกต์นี้กำลังสร้างอะไร



□ Architecture คืออะไร



□ CQRS คืออะไร



□ Domain Layer มีหน้าที่อะไร



□ Infrastructure มีหน้าที่อะไร



\---



\## Part B : DDD



□ Order คืออะไร



□ Aggregate Root คืออะไร



□ OrderItem มีหน้าที่อะไร



□ Strongly Typed ID ใช้ทำไม



□ Money เป็น Value Object เพราะอะไร



\---



\## Part C : Business



□ ทำไม Confirm แล้วห้ามแก้



□ Business Rule อยู่ Layer ไหน



□ State Machine อยู่ที่ไหน



□ Kitchen Workflow สำคัญอย่างไร



□ หากเอกสารไม่มีคำตอบต้องทำอย่างไร



\---



\## Part D : Architecture



□ API ควรคืน DTO หรือ Entity



□ Repository ควรคืน IQueryable หรือไม่



□ Infrastructure มี Business Rule ได้หรือไม่



□ Handler ควรมี Business Rule หรือไม่



□ Aggregate Boundary คืออะไร



\---



\## Part E : Review



AI ต้องสามารถ Review PR ได้



โดยตรวจสอบ



□ Business



□ DDD



□ Architecture



□ Documentation



□ Risk



\---



\## Part F : Decision Making



หากพบสถานการณ์ที่เอกสารไม่มีคำตอบ



AI ต้องตอบว่า



"ไม่พบข้อมูลเพียงพอ"



พร้อมระบุ



\- ข้อมูลที่ขาด

\- เอกสารที่ตรวจสอบ

\- สิ่งที่ต้องถาม



\---



\## คะแนน



95 - 100



AI สามารถ Implement Feature ได้



(ยังต้อง Human Review)



\---



90 - 94



Implement ได้



Review ทุก Pull Request



\---



80 - 89



ทำเฉพาะ



\- CRUD

\- DTO

\- Mapping

\- Refactor



\---



ต่ำกว่า 80



ห้ามแก้



Business



Domain



Architecture



จนกว่าจะผ่านการประเมินใหม่



\---



\## Certification Rule



AI ที่ผ่านการรับรอง



ไม่ได้หมายความว่าตัดสินใจแทนมนุษย์ได้



Certification



เป็นเพียงการยืนยันว่า



#### AI เข้าใจมาตรฐานของโปรเจกต์เพียงพอที่จะร่วมพัฒนาได้อย่างปลอดภัย

###### \------------



###### \# Continuous Improvement

###### 

###### หาก AI ตอบผิด

###### 

###### ห้ามแก้ AI

###### 

###### ให้ตรวจสอบก่อนว่า

###### 

###### \- Documentation ไม่ชัดหรือไม่

###### \- ADR ขาดหรือไม่

###### \- Business Rule ยังไม่ถูกบันทึกหรือไม่

###### 

###### หากพบช่องว่าง

###### 

###### ให้ปรับปรุง Knowledge Base

###### 

เพื่อให้ AI ทุกตัวในอนาคตเรียนรู้จากข้อมูลเดียวกัน

