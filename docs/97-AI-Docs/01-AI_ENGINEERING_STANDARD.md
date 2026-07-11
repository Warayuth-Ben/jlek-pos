\# AI Engineering Standard

Version: 1.0

Project: JLek POS



\---



\## Purpose



เอกสารนี้กำหนดมาตรฐานการทำงานของ AI ทุกตัวที่เข้ามามีส่วนร่วมในโปรเจกต์ JLek POS



AI ทุกตัวต้องปฏิบัติตามมาตรฐานนี้โดยไม่ขึ้นกับผู้ให้บริการ (ChatGPT, Cline, Codex, Claude, Gemini หรือ AI อื่น)



หากมาตรฐานนี้ขัดแย้งกับความรู้ทั่วไปของ AI ให้ถือว่ามาตรฐานของโปรเจกต์เป็นหลัก



\---



\# Standard 1 - Documentation First



ก่อนวิเคราะห์ ออกแบบ หรือเขียนโค้ด



AI ต้องอ่านเอกสารที่เกี่ยวข้องก่อนเสมอ



ห้ามตอบจากความจำของโมเดลเพียงอย่างเดียว



\---



\# Standard 2 - Never Guess



หากเอกสารไม่มีข้อมูลเพียงพอ



ให้ตอบว่า



"ไม่พบข้อมูลเพียงพอ"



พร้อมระบุ



\- ข้อมูลที่ขาด

\- เอกสารที่ตรวจสอบแล้ว

\- สิ่งที่ต้องสอบถาม



ห้ามสันนิษฐาน Business Rule



\---



\# Standard 3 - Business Before Code



ก่อนเขียนโค้ด



AI ต้องเข้าใจ



\- Business Problem

\- Business Rule

\- Workflow



หากอธิบายไม่ได้



ห้ามเขียนโค้ด



\---



\# Standard 4 - Explain Before Implement



ก่อน Implement



AI ต้องอธิบาย



\- วิธีแก้

\- เหตุผล

\- ผลกระทบ

\- ความเสี่ยง

\- เอกสารอ้างอิง



\---



\# Standard 5 - Architecture First



Architecture สำคัญกว่า Code



Business สำคัญกว่า Technology



Code เป็นผลลัพธ์ของการออกแบบ



\---



\# Standard 6 - Documentation is Source of Truth



เมื่อเอกสารขัดกับความรู้ทั่วไป



Documentation ชนะเสมอ



\---



\# Standard 7 - Human Review



AI ไม่มีสิทธิ์ตัดสินใจแทนมนุษย์ในเรื่อง



\- Business Rule

\- Architecture

\- Aggregate

\- State Machine



AI มีหน้าที่เสนอ



มนุษย์มีหน้าที่ตัดสินใจ



\---



\# Standard 8 - Self Review



ก่อนส่งงาน



AI ต้องตรวจสอบตัวเอง



\- Business Rule

\- DDD

\- Architecture

\- API Contract

\- Documentation



\---



\# Standard 9 - Documentation Update



หากมีการเปลี่ยนแปลง



\- Business Rule

\- Workflow

\- Architecture



AI ต้องเสนอให้อัปเดตเอกสาร



\---



\# Standard 10 - Reviewability



ทุกคำตอบต้องสามารถตรวจสอบย้อนกลับได้



###### ทุกข้อเสนอควรมีเหตุผลและเอกสารอ้างอิง

