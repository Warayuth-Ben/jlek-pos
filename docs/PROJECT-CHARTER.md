\# Project Charter



This repository is not only a software project. It is also a long-term knowledge preservation project.

Repository นี้ไม่ได้มีเป้าหมายเพียงการพัฒนาซอฟต์แวร์ แต่มีเป้าหมายในการเก็บรักษา ถ่ายทอด และส่งต่อองค์ความรู้ของร้านอาหารควบคู่ไปกับการพัฒนาระบบ



Project



JLek POS



Version



1.0



Status



Active



\---



\# Purpose



JLek POS เป็นโครงการพัฒนาระบบ Point of Sale (POS) สำหรับร้านอาหาร



โครงการนี้ไม่ได้มุ่งเน้นเพียงการสร้างซอฟต์แวร์ แต่มีเป้าหมายในการสร้างองค์ความรู้ที่สามารถส่งต่อและพัฒนาต่อยอดได้ในระยะยาว



ประกอบด้วย



\- Software Architecture

\- Documentation

\- Development Standards

\- Knowledge Book



ทั้งสี่ส่วนมีเป้าหมายร่วมกัน คือการรักษาความถูกต้องของธุรกิจ และถ่ายทอดวิธีคิดในการออกแบบระบบ



\---



\# Vision



สร้างระบบ POS ที่สะท้อนการทำงานจริงของร้านอาหาร



พร้อมทั้งสร้างองค์ความรู้ที่สามารถส่งต่อไปยังคนรุ่นต่อไปได้



เราไม่ได้สร้างเพียงระบบ



แต่กำลังบันทึกวิธีคิดในการบริหารร้านและการออกแบบซอฟต์แวร์



\---



\# Mission



Business มาก่อน Technology



Workflow มาก่อน Source Code



Reasoning มาก่อน Implementation



Knowledge มาก่อน Feature



\---



\# Core Principles



\## Business-first



Business เป็นตัวกำหนด Architecture



ไม่ใช่ Technology



\---



\## Domain-Driven Design



แบบจำลองของระบบต้องสะท้อนโลกจริงของร้านอาหาร



\---



\## Clean Architecture



Business Logic ต้องไม่ขึ้นกับ Framework



Database



UI



หรือ Technology



\---



\## Knowledge Preservation



ความรู้ที่ไม่ได้ถูกบันทึก



คือความรู้ที่กำลังจะหายไป



โครงการนี้จึงให้ความสำคัญกับการเก็บรักษาองค์ความรู้ควบคู่กับการพัฒนาซอฟต์แวร์



\---



\# Repository Structure



Repository นี้แบ่งออกเป็น 4 ส่วนหลัก



\## 1. Specification



โฟลเดอร์



00–10



ใช้สำหรับกำหนดรายละเอียดของระบบ



Specification ถือเป็นแหล่งข้อมูลที่ถูกต้องที่สุด (Single Source of Truth)



\---



\## 2. Development Handbook



โฟลเดอร์



11-development



กำหนดมาตรฐานการพัฒนา



Coding Standards



Git Workflow



Definition of Done



Architecture Rules



และแนวปฏิบัติของทีมพัฒนา



\---



\## 3. Knowledge Book



โฟลเดอร์



21-knowledge-book



หนังสือที่บันทึก Workflow



เหตุผล



ประสบการณ์



และองค์ความรู้ของร้าน



หนังสือชุดนี้มีหน้าที่อธิบาย



ไม่ใช่กำหนด Requirement



\---



\## 4. References



98-decisions



99-references



ใช้สำหรับเก็บข้อมูลสนับสนุน



Architecture Decision Records



และข้อมูลอ้างอิง



\---



\# Documentation Hierarchy



Business



↓



Architecture



↓



Specification



↓



Implementation



↓



Knowledge Book



Knowledge Book ต้องอ้างอิง Specification เสมอ



Specification ถือเป็นข้อมูลที่ถูกต้องที่สุด



\---



\# Project Philosophy



เราเชื่อว่า



ซอฟต์แวร์ที่ดี



ไม่ได้เริ่มจากการเขียนโค้ด



แต่เริ่มจากการเข้าใจธุรกิจ



และเข้าใจผู้คน



\---



\# Conversation Modes



เพื่อให้การทำงานมีความชัดเจน



สามารถกำหนดบทบาทของการสนทนาได้



PM:



วิเคราะห์ธุรกิจ



Requirement



Architecture



\---



ARCH:



Architecture Review



DDD



Clean Architecture



\---



DEV:



Implementation



Refactoring



Debugging



\---



TEST:



Testing



Quality Assurance



\---



DOC:



Documentation



Standards



Guides



\---



BOOK:



Knowledge Book



Storytelling



Knowledge Preservation



\---



BRAIN:



Brainstorm



Idea Exploration



ยังไม่ตัดสินใจ



\---



\# Working Principle



PM



สร้างความจริงของระบบ



↓



Specification



กำหนดรายละเอียดของระบบ



↓



DEV



พัฒนาระบบ



↓



TEST



ตรวจสอบคุณภาพ



↓



DOC



ปรับปรุงเอกสาร



↓



BOOK



ถ่ายทอดองค์ความรู้



\---



\# Long-term Goal



เป้าหมายสูงสุดของโครงการไม่ใช่เพียงการมีระบบ POS ที่ทำงานได้



แต่คือการสร้าง



\- ระบบที่ดูแลรักษาง่าย

\- สถาปัตยกรรมที่ขยายต่อได้

\- เอกสารที่เป็นมาตรฐาน

\- หนังสือที่เก็บรักษาองค์ความรู้ของร้าน



เพื่อให้คนรุ่นต่อไปสามารถเข้าใจทั้งระบบและเหตุผลเบื้องหลังการออกแบบได้



\---



\# Motto



อย่าบันทึกเพียงว่า



"ระบบทำอะไร"



แต่จงบันทึกว่า



"ร้านคิดอย่างไร"



\---



\# Final Statement



ทุกระบบมี Source Code



แต่ไม่ใช่ทุกระบบจะมีเรื่องราว



JLek POS จะมีทั้งสองอย่าง

