###### \# AI Context


> \*\*Read this document before reading any other documentation in this repository.\*\*
---

###### >

###### > It provides the context required to correctly understand the purpose, structure, and philosophy of the JLek POS project.

###### 

###### This document provides high-level context for AI assistants working with the JLek POS repository.

###### 

###### It explains the purpose of the project, the documentation structure, and the expected workflow.

###### 

###### \---

###### 

###### \# Project Summary

###### 

###### JLek POS is a Point of Sale (POS) system for restaurants.

###### 

###### However, this repository is more than a software project.

###### 

###### It is also a long-term knowledge preservation project.

###### 

###### The goal is not only to build software, but also to preserve business knowledge, architectural decisions, and real-world operational experience.

###### 

###### \---

###### 

###### \# Project Philosophy

###### 

###### Always remember:

###### 

###### Business comes before Technology.

###### 

###### Workflow comes before Source Code.

###### 

###### Reasoning comes before Implementation.

###### 

###### Knowledge comes before Features.

###### 

###### When making design decisions, always understand the business problem before proposing technical solutions.

###### 

###### \---

###### 

###### \# Repository Structure

###### 

###### The repository is organized into several major sections.

###### 

###### \## 00–10

###### 

###### System Specification

###### 

###### Contains the official software specification.

###### 

###### Includes:

###### 

###### \- Standards

###### \- Business Rules

###### \- Domain Model

###### \- Use Cases

###### \- State Machines

###### \- Application

###### \- Technical Design

###### \- Database

###### \- API

###### \- UI

###### \- Testing

###### 

###### This is the Single Source of Truth.

###### 

###### Never contradict these documents.

###### 

###### \---

###### 

###### \## 11-development

###### 

###### Development Handbook

###### 

###### Contains

###### 

###### \- Coding Standards

###### \- Git Workflow

###### \- Definition of Done

###### \- Development Process

###### \- Best Practices

###### 

###### \---

###### 

###### \## 21-knowledge-book

###### 

###### Story-driven Knowledge Book.

###### 

###### This section is NOT a specification.

###### 

###### Its purpose is to explain

###### 

###### \- business workflow

###### \- architectural reasoning

###### \- operational experience

###### \- historical decisions

###### 

###### through real restaurant scenarios.

###### 

###### This book explains the system.

###### 

###### It never defines the system.

###### 

###### \---

###### 

###### \## 98-decisions

###### 

###### Architecture Decision Records (ADR)

###### 

###### Historical decisions.

###### 

###### \---

###### 

###### \## 99-references

###### 

###### External references.

###### 

###### Research materials.

###### 

###### Supporting documents.

###### 

###### \---

###### 

###### \# Documentation Hierarchy

###### 

###### Business

###### 

###### ↓

###### 

###### Architecture

###### 

###### ↓

###### 

###### Specification

###### 

###### ↓

###### 

###### Implementation

###### 

###### ↓

###### 

###### Knowledge Book

###### 

###### Knowledge Book must always follow the Specification.

###### 

###### Never introduce new business rules inside the Knowledge Book.

###### 

###### \---

###### 

###### \# Knowledge Book Philosophy

###### 

###### The Knowledge Book is not

###### 

###### \- Documentation

###### \- API Guide

###### \- User Manual

###### \- Software Specification

###### 

###### It is

###### 

###### \- Knowledge Preservation

###### \- Story-driven Handbook

###### \- Business Narrative

###### \- Architecture Story

###### 

###### Its purpose is to preserve the knowledge behind the system.

###### 

###### \---

###### 

###### \# Writing Principles

###### 

###### When writing Knowledge Book chapters:

###### 

###### Always start from

###### 

###### a real business situation.

###### 

###### Never start from technology.

###### 

###### Explain the same event from three perspectives.

###### 

###### 1\. Business Perspective

###### 

###### What happens inside the restaurant?

###### 

###### 2\. System Perspective

###### 

###### What does the software do?

###### 

###### 3\. Architecture Perspective

###### 

###### Why was the system designed this way?

###### 

###### Focus on "Why", not only "How".

###### 

###### \---

###### 

###### \# AI Responsibilities

###### 

###### When assisting this project:

###### 

###### Do

###### 

###### \- respect the Specification

###### \- preserve architectural consistency

###### \- explain reasoning

###### \- use restaurant examples

###### \- identify trade-offs

###### \- think from the business perspective first

###### 

###### Do NOT

###### 

###### \- invent business rules

###### \- contradict specifications

###### \- redesign architecture without justification

###### \- optimize prematurely

###### \- introduce unnecessary complexity

###### 

###### \---

###### 

###### \# Conversation Modes

###### 

###### The user may explicitly switch working modes.

###### 

###### PM:

###### 

###### Business analysis

###### 

###### Requirement

###### 

###### Architecture

###### 

###### Decision making

###### 

###### \---

###### 

###### ARCH:

###### 

###### Architecture review

###### 

###### DDD

###### 

###### Clean Architecture

###### 

###### System design

###### 

###### \---

###### 

###### DEV:

###### 

###### Implementation

###### 

###### Code review

###### 

###### Refactoring

###### 

###### Debugging

###### 

###### \---

###### 

###### TEST:

###### 

###### Testing

###### 

###### Quality Assurance

###### 

###### Acceptance Criteria

###### 

###### \---

###### 

###### DOC:

###### 

###### Documentation

###### 

###### README

###### 

###### Guidelines

###### 

###### Standards

###### 

###### \---

###### 

###### BOOK:

###### 

###### Knowledge Book

###### 

###### Storytelling

###### 

###### Knowledge Preservation

###### 

###### Architecture Narrative

###### 

###### \---

###### 

###### BRAIN:

###### 

###### Brainstorming

###### 

###### Explore ideas

###### 

###### No final decisions

###### 

###### \---

###### 

###### \# Long-term Vision

###### 

###### The repository aims to preserve not only source code, but also business knowledge.

###### 

###### Software can be rewritten.

###### 

###### Frameworks can change.

###### 

###### Programming languages can evolve.

###### 

###### Business knowledge should remain.

###### 

###### The ultimate goal is that future developers and business owners can understand not only how the system works, but also why it was designed this way.

###### 

###### \---

###### 

###### \# Final Reminder

###### 

###### Every software project has source code.

###### 

###### Not every software project preserves its knowledge.

###### 

###### JLek POS aims to preserve both.

