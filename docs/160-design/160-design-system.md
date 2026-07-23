# 160 — Design System

Status: Canonical Edition
Version: 1.0
State: FROZEN
Last Approved: 2026-07-23

Series: Design (160–199)

Canonical Source: docs/160-design/160-design-system.md

---

# Section 1: Introduction

---

## 1.1 Purpose

The JLek POS project did not begin with a design tool, a UI framework, or a component library. It began with understanding how a restaurant operates. Before any line of code was written, before any interaction was conceived, the business was studied first. This document continues that same order: business before design, understanding before specification.

What follows is the Design Constitution of the JLek POS system. It establishes the foundational principles that guide every design decision — whether made by a human or an AI. Concrete visual values lie outside its remit. Instead it defines the precepts from which those values are later inferred. Every tenet traces back to the Business Foundation. None are invented. None are borrowed from generic design philosophy.

The purpose is straightforward: to ensure that every design decision across the entire system remains consistent with the restaurant's operational philosophy.

---

## 1.2 Scope

Only design principles belong here. Concrete visual design values do not. Colors, typography, spacing, components, implementation details — every topic that falls outside this scope has a canonical owner elsewhere in the documentation. This is a Design Constitution, not a UI Guideline, Component Library, Implementation Guide, or Style Guide.

---

## 1.3 Audience

Four audiences: designers creating Design Standards, AI systems generating design content, engineers implementing components and workflows, and reviewers verifying design consistency.

---

## 1.4 Position within the Documentation Series

This document is the Design Constitution (160). Above it are Business Foundation and Architecture. Below it are Design Standards (161–199). Dependencies flow downward. This hierarchy is not negotiable.

---

## 1.5 Relationship with Business Foundation

Six business philosophies form the foundation: customer before system, restaurant before software, kitchen is the boundary, payment is not the end, service recovery is a business requirement, information hierarchy drives operations. Each produces a corresponding design principle.

---

## 1.6 Relationship with Architecture

Four architectural constraints are relevant: aggregate boundaries determine information boundaries, cross-context communication by identity only, CQRS separates command and query paths, layer separation prevents dependency inversion.

---

## 1.7 Relationship with Design Standards

Design Standards (161–199) translate constitutional principles into concrete specifications. Four rules govern this relationship. If a Design Standard conflicts with this document, this document prevails.

---

## 1.8 What This Document Does NOT Contain

Seventeen excluded topics with canonical owners. Colors, Typography, Spacing, Components, CSS, HTML, Blazor, and implementation details are explicitly outside scope.

---

## 1.9 Related Documents

Fourteen related documents with dependency direction.

---

# Section 2: Design Philosophy

---

## 2.1 The Origin of Design Principles

Design principles originate from the Business Foundation. They are derived, not invented. No principle is negotiable at the design level.

---

## 2.2 Business Philosophy Precedes Design Philosophy

Business philosophy is parent to design philosophy. Design does not create new business realities. Business fidelity is more important than design sophistication.

---

## 2.3 Restaurant Operations Shape System Behavior

Restaurant service happens in real time. The system must never become the bottleneck. These constraints exist because operations demand them.

---

## 2.4 Customer Experience Is the Consequence of Operational Design

Customer experience is operational, not visual. Design quality is measured by operational efficiency, not aesthetic appeal.

---

## 2.5 The Six Foundational Design Principles

Customer Before System, Restaurant Before Software, Kitchen Is The Boundary, Payment Is Not The End, Service Recovery, Information Hierarchy.

---

## 2.6 Consistency as a Foundational Requirement

Consistency does not mean visual uniformity. Every design decision must be traceable to the same foundational principles.

---

# Section 3: Information Architecture

---

## 3.1 Why Information Architecture Exists

Information architecture defines how information is structured, prioritized, and presented according to operational need.

---

## 3.2 Information Hierarchy Rules

Three rules: operational importance determines prominence, frequency of use determines depth of access, urgency determines interrupt priority.

---

## 3.3 Priority of Content Over Decoration

Content always takes priority. Every visual choice must be justified by its contribution to information clarity.

---

## 3.4 Contextual Information Layering

Three layers: primary (task completion), secondary (task context), tertiary (reference). Higher layers never obscure lower ones.

---

## 3.5 Cross-Feature Information Consistency

Same concept same representation, same status same indication, same action same behavior.

---

## 3.6 Information Integrity Across Operational States

Information meaning does not change based on operational context. The information model remains stable regardless of load.

---

## 3.7 Constitutional Consequences

Information architecture principles cascade into every subsequent design decision. No Design Standard may violate them.

---

# Section 4: Layout Architecture

---

## 4.1 Why Layout Architecture Exists

Layout architecture determines how informational priority is expressed in space.

---

## 4.2 Content-First Layout

Layout serves content, not the reverse.

---

## 4.3 Progressive Disclosure

Information is presented in stages matching the operational workflow.

---

## 4.4 Spatial Relationships Communicate Operational Relationships

Proximity expresses association. Separation expresses independence. Alignment supports cognitive efficiency.

---

## 4.5 Layout Reduces Cognitive Load During Operations

Layout reduces cognitive load through consistent positioning, visual grouping, and reduced scanning distance.

---

## 4.6 Layout Consistency Across Operational Contexts

The same spatial principles are applied across all contexts even when resulting layouts differ.

---

## 4.7 Feature-Scoped Layout Ownership

Each feature owns its layout decisions within constitutional constraints.

---

## 4.8 Constitutional Constraints

Five constitutional constraints govern every layout decision. A Layout Standard that conflicts with any is invalid.

---

# Section 5: Interaction Philosophy

---

## 5.1 Why Interaction Philosophy Exists

Interaction philosophy governs what happens when the staff member acts. Every interaction either supports or disrupts operational continuity.

---

## 5.2 Service Before Data Completeness

Service action precedes data entry. The system must never interrupt service to demand data completeness.

---

## 5.3 Error Recovery as a First-Class Interaction Concern

Every primary path must have an equally accessible recovery path.

---

## 5.4 Feedback Without Interruption

Feedback must inform without demanding acknowledgment.

---

## 5.5 Undo Before Confirmation

Reversible actions execute immediately with undo. Confirmation is driven by operational risk, not frequency.

---

## 5.6 Context Preservation Across Operations

Interrupted interactions must retain their state.

---

## 5.7 Interaction Responsibility for Irreversible Actions

Irreversible actions require clear indication, summary display, and proportional confirmation.

---

## 5.8 Minimizing Interaction Cost

Interaction cost must be proportional to operational value across discovery, execution, and verification.

---

## 5.9 Constitutional Constraints

Seven constitutional constraints govern every interaction decision. An Interaction Standard that violates any is invalid.

---

# Section 6: Visual Language

---

## 6.1 Why Visual Language Exists

Visual language is where business philosophy becomes perceptible. It determines how the system looks for the sake of recognition, not appearance.

---

## 6.2 Purpose of Visual Emphasis

Visual emphasis must communicate operational importance. No element may be given prominence unless operational importance justifies it.

---

## 6.3 Purpose of Color

Color serves three operational purposes: status indication, urgency signaling, and category differentiation. Decorative color is prohibited.

---

## 6.4 Purpose of Typography

Typography serves readability and hierarchy. Typographic variation must be justified by operational need.

---

## 6.5 Purpose of Spacing

Spacing expresses the strength of operational relationships between elements.

---

## 6.6 Visual Hierarchy Supporting Information Hierarchy

Visual hierarchy must be consistent across all features. The same operational importance must receive the same visual treatment everywhere.

---

## 6.7 Consistency of Visual Language

Visual language is a system-wide property. No feature may invent its own vocabulary or repurpose visual signals.

---

## 6.8 Visual Language Subordinate to Business Meaning

Any visual rule may be broken when operational understanding requires it. Business meaning overrides visual consistency.

---

## 6.9 Constitutional Constraints

Six constitutional constraints govern every visual decision. Color, Typography, and Spacing Standards that conflict with these principles are invalid.

---

# Section 7: Component Philosophy

---

## 7.1 Why Component Philosophy Exists

Components are where all preceding constraints converge into tangible units of operational responsibility.

---

## 7.2 Component Responsibility Boundaries

Every component must have a single operational responsibility traceable to the Business Foundation.

---

## 7.3 Composition Over Configuration

Prefer composition over configuration. Composition preserves clarity. Configuration increases complexity.

---

## 7.4 Feature Ownership Over Cross-Feature Coupling

Features own their components. Shared concepts are system-owned.

---

## 7.5 Component Contract Principles

Every component has an information contract, a behavior contract, and a visual contract.

---

## 7.6 When to Create a Component

Create components for stable, reusable operational responsibilities. Defer creation for exploratory needs.

---

## 7.7 Constitutional Constraints

Five constitutional constraints govern every component decision. A Component Standard that conflicts with any is invalid.

---

# Section 8: Design Governance

---

## 8.1 Why Design Governance Exists

Seven chapters have established the constitutional principles that govern every design decision in this system. Information architecture, layout architecture, interaction philosophy, visual language, and component philosophy each define constraints that must be simultaneously satisfied. Design governance is the mechanism that ensures these constraints remain satisfied over time.

A constitution that cannot be enforced is not a constitution. It is a collection of suggestions. Every constitutional principle in this document would be meaningless if a future Design Standard could override it without justification. Governance exists to prevent that erosion.

Governance does not exist for administrative control. It does not exist to slow down design decisions or to create bureaucratic overhead. It exists for a single purpose: to preserve the integrity of the design as the system evolves. As new features are added, as new Design Standards are written, as new team members join the project — the constitutional principles must remain intact. Governance ensures that they do.

This section establishes the constitutional rules for maintaining constitutional integrity. It defines how Design Standards derive their authority, how conflicts between standards are resolved, how constitutional principles evolve, and how long-term coherence is protected.

---

## 8.2 Design Authority

Every Design Standard (161–199) derives its authority from this Constitution. A Design Standard is valid only to the extent that it is consistent with the principles established in the preceding chapters.

This authority is not merely philosophical. It has a concrete consequence: when a Design Standard is being created or modified, the creator must verify that it is consistent with every constitutional principle that applies to its domain. A Color Standard must be verified against the visual language principles in Section 6. A Layout Standard must be verified against the layout architecture principles in Section 4. An Interaction Standard must be verified against the interaction philosophy principles in Section 5. A Component Standard must be verified against the component philosophy principles in Section 7.

The principle that emerges: a Design Standard must document its constitutional lineage. For each specification it defines, it must reference the constitutional principle from which that specification is derived. A Design Standard that cannot trace its specifications back to constitutional principles is a Design Standard without foundation. It may be aesthetically pleasing, internally consistent, or technically elegant. But it has no authority.

---

## 8.3 Local Optimization Must Preserve System-Wide Consistency

A feature team optimizing its own domain may make decisions that improve local efficiency but degrade system-wide consistency. The ordering feature may introduce a new representation for order status that works well for ordering but conflicts with how status is represented in the kitchen feature. The payment feature may introduce a new visual treatment that works well for payments but violates the visual language established in Section 6.

Local optimization that violates system-wide consistency is unacceptable. The constitutional principles established in this document define the boundaries within which local optimization may occur. A feature team may optimize its domain, but it may not violate information hierarchy, cross-feature consistency, layout principles, interaction philosophy, visual language, or component philosophy.

The principle that emerges: local optimization is bounded by constitutional constraints. A feature team may improve efficiency within its domain, but only if the improvement does not violate any constitutional principle. If a conflict arises between local efficiency and system-wide consistency, system-wide consistency prevails.

This principle has a specific implication for Design Standards. A Design Standard that defines specifications for a specific domain must be verified against the constitutional principles that govern all domains. If a domain-specific standard would violate a system-wide principle, the standard must be revised, not the principle.

---

## 8.4 Constitutional Principles Evolve Slowly

The constitutional principles in this document are not immutable. Business requirements change. Operational realities shift. New understanding emerges. When these changes occur, the constitutional principles may need to evolve.

But constitutional evolution is fundamentally different from standard evolution. A Design Standard may be updated frequently as implementation approaches improve. A color value may change. A spacing scale may be adjusted. A layout grid may be refined. These are standard-level changes that do not affect the constitution.

A constitutional principle changes only when the business philosophy that underlies it changes. The principle that service must precede data completeness is derived from the business philosophy that the customer comes before the system. If that business philosophy were to change — if the restaurant decided that data completeness should take priority over service — the constitutional principle would change. But that is a business decision, not a design decision.

The principle that emerges: changes to constitutional principles require stronger justification than changes to Design Standards. A Design Standard change requires evidence that the new specification is more effective. A constitutional change requires evidence that the underlying business philosophy has changed. The threshold for constitutional change is higher.

Constitutional principles should be reviewed periodically, but they should not change frequently. A principle that changes every year is not a principle. It is a preference.

---

## 8.5 Conflict Resolution

Conflicts between Design Standards will inevitably arise. A Layout Standard may define a spatial arrangement that conflicts with an Interaction Standard's requirements for feedback placement. A Color Standard may define a palette that conflicts with a Component Standard's requirements for status indication. A Typography Standard may define a scale that conflicts with a Visual Language principle about hierarchy.

When conflicts arise, the resolution must follow a clear hierarchy.

**Constitutional principles take precedence over Design Standards.** If a Design Standard conflicts with a constitutional principle, the Design Standard is invalid. The constitutional principle stands. This is the foundation of the entire governance framework.

**Earlier constitutional chapters take precedence over later chapters in cases of direct conflict.** The information architecture principles in Section 3 take precedence over the visual language principles in Section 6 when they directly conflict, because information structure is more fundamental than visual expression. However, such conflicts should be rare. The chapters are designed to be consistent with each other.

**Design Standards that are closer to the business foundation take precedence over standards that are farther.** A Component Standard that defines component boundaries takes precedence over a Color Standard that defines component colors, because component boundaries are more fundamental to operational meaning than component appearance.

The principle that emerges: conflicts are resolved by tracing back to the most fundamental level. The Business Foundation is the ultimate authority. Constitutional principles derived from the Business Foundation are the next authority. Design Standards are subordinate to both. When a conflict cannot be resolved at the standard level, it must be escalated to the constitutional level. When it cannot be resolved at the constitutional level, it must be escalated to the Business Foundation.

---

## 8.6 Standards Evolve, Principles Remain Stable

Constitutional principles and Design Standards operate at different rates of change. Design Standards evolve frequently as implementation approaches improve, as new technologies become available, and as operational understanding deepens. Constitutional principles evolve rarely, only when the underlying business philosophy changes.

This difference in evolution rate is intentional and essential. If constitutional principles changed as frequently as Design Standards, they would not provide a stable foundation. Every Design Standard change would require reevaluating the entire constitutional framework. No team could work independently because the ground would shift beneath them.

The principle that emerges: stability of constitutional principles enables flexibility of Design Standards. When a team knows that the constitutional principles will not change, they can confidently create Design Standards that comply with those principles. They do not need to worry that the foundation will shift while they are building.

This means that Design Standards should be written to comply with constitutional principles as they are, not as they might be. A Design Standard should not anticipate future constitutional changes. It should be consistent with the constitution that exists today. If the constitution changes in the future, the Design Standard may need to be updated — but that is a future concern.

---

## 8.7 Architectural Consistency as Shared Responsibility

Every constitutional principle in this document was created to serve a single purpose: to ensure that the system remains consistent with the restaurant's operational philosophy. That consistency is not the responsibility of a single person, a single team, or a single role. It is a shared responsibility across the entire project.

Designers creating Design Standards are responsible for ensuring that their standards comply with constitutional principles. They must read this document, understand its principles, and verify that their specifications are derived from those principles. A designer who creates a standard without understanding the constitution is designing without foundation.

AI systems generating design content are responsible for ensuring that their output complies with constitutional principles. The Generation Prompt (docs/160-design/160-generation-prompt.md) requires this verification explicitly. An AI that generates content without constitutional compliance is generating without authority.

Engineers implementing components and workflows are responsible for ensuring that their implementations respect constitutional principles. An engineer who implements a component that conflates operational responsibilities, that violates cross-feature consistency, or that ignores visual language principles is implementing without integrity.

Reviewers evaluating design decisions are responsible for verifying constitutional compliance. A reviewer who approves a Design Standard that contradicts a constitutional principle is approving without diligence.

The principle that emerges: architectural consistency is everyone's responsibility. No single role can maintain it alone. Every contributor to the design — human or AI — must understand the constitutional principles and verify that their work is consistent with them. The constitution is a shared document, and its integrity depends on shared commitment.

---

## 8.8 Constitutional Constraints

The governance principles established in this section are themselves constitutional constraints.

Design Authority means that every Design Standard must document its constitutional lineage. A Design Standard that cannot trace its specifications back to constitutional principles lacks authority.

Local optimization must preserve system-wide consistency. A design decision that improves local efficiency at the expense of system-wide consistency violates this principle. Constitutional constraints bound local optimization.

Constitutional principles evolve slowly. A change to a constitutional principle requires evidence that the underlying business philosophy has changed. The threshold for constitutional change is higher than the threshold for standard change.

Conflict resolution follows hierarchy. Constitutional principles take precedence over Design Standards. Earlier chapters take precedence over later chapters in direct conflicts. Business Foundation is the ultimate authority.

Standards evolve, principles remain stable. Design Standards may change frequently. Constitutional principles change rarely. Stability of principles enables flexibility of standards.

Architectural consistency is shared responsibility. Every contributor — designer, AI, engineer, reviewer — is responsible for constitutional compliance.

These governance constraints have a specific consequence for the Design Standards. A Design Standard that is created without verifying constitutional compliance, that is optimized locally at the expense of system-wide consistency, or that attempts to change a constitutional principle without business justification, is invalid. The constitution constrains the standards absolutely.

The final chapter affirms the enduring role of this Design Constitution, its relationship with the Design Standards that follow, and the constitutional authority that will guide every future design decision.

---

# Section 9: Conclusion

---

## 9.1 The Enduring Role of the Design Constitution

The JLek POS project began with a question: how does a restaurant operate? Before any design tool was opened, before any interface was conceived, before any component was imagined, the business was studied. The answer to that question is recorded in the Business Foundation. This document is the bridge from that answer to every design decision that follows.

A design constitution is not a static artifact. It is a living foundation — stable enough to provide consistent guidance across decades of implementation, yet principled enough to accommodate evolution in business understanding. It does not prescribe specific colors, typefaces, or spacing values. It does not define which framework to use or how to structure a component's implementation. It defines the principles that govern those decisions. The specifics belong to the Design Standards, which may change as technology advances. The principles belong here, and they endure.

The Design Constitution is the highest design authority of the JLek POS project. Every future Design Standard — whether it defines color values, typographic scales, component boundaries, or interaction patterns — derives its legitimacy from the principles established in this document. A Design Standard that is consistent with these principles is valid. A Design Standard that contradicts them is not. This authority is not administrative. It is constitutional.

Design Standards will evolve. New color palettes will replace old ones. New typographic scales will be calibrated as display technology improves. New component patterns will emerge as operational understanding deepens. Each of these changes is a normal part of a healthy design system. The Design Constitution does not resist this evolution. It enables it by providing a stable foundation against which every change can be evaluated.

Constitutional stability enables evolution because it provides certainty. When a team creating a new Design Standard knows that the constitutional principles will not shift beneath them, they can focus on creating the best possible specification within those constraints. When a team implementing a component knows that the visual language principles will remain consistent, they can build with confidence. The constitution is the fixed point around which everything else evolves.

---

## 9.2 Design Quality Is Constitutional Consistency

The measure of design quality in this system is not visual novelty, aesthetic appeal, or adherence to current trends. The measure of design quality is constitutional consistency. A design decision is good if it is consistent with the principles established in this document. It is bad if it contradicts them, regardless of how attractive or innovative it may appear.

This measure has a specific consequence for how design decisions are evaluated. When a new Design Standard is proposed, the first question must not be "does this look good?" The first question must be "is this consistent with the Design Constitution?" A standard that passes constitutional review may then be evaluated for effectiveness, efficiency, and appeal. A standard that fails constitutional review must be revised before any further evaluation occurs.

Constitutional consistency also provides protection against design trends. A trend that conflicts with the principle that content must take priority over decoration cannot be adopted, regardless of its popularity. A trend that conflicts with the principle that service must precede data completeness cannot be adopted, regardless of its convenience. The constitution insulates the system from the volatility of design fashion.

---

## 9.3 Beyond Technology and Framework

The Design Constitution is intentionally independent of technology. It does not reference Blazor, CSS, HTML, or any specific framework. It does not prescribe implementation patterns, database schemas, or API conventions. It operates at a level of abstraction that transcends any particular technology stack.

This independence is deliberate. Technologies change. Frameworks are replaced. Implementation patterns evolve. But the operational reality of a restaurant — the need for service continuity, the boundary between front of house and kitchen, the importance of information hierarchy — changes slowly, if at all. The Design Constitution is written for the operational reality, not for the current technology.

When a new framework emerges, the Design Standards may need to be updated to express the same constitutional principles in the new context. But the constitution itself does not change. A component philosophy that governs how responsibilities are allocated is as applicable to a new framework as it was to the old one. A visual language principle that governs the purpose of color is as relevant in a new rendering technology as it was in the previous one.

The Design Constitution is intended to outlive individual technologies, frameworks, and interface trends. It will still be relevant when the current user interface paradigm has been replaced by something unforeseen. Because it is grounded in business philosophy rather than technology, it can provide guidance across generations of implementation.

---

## 9.4 The Ultimate Authority

The Design Constitution is the highest design authority, but it is not the highest authority overall. Above it stands the Business Foundation. Every principle in this document is derived from the business philosophies documented there. If a conflict arises that cannot be resolved within the constitutional framework, the Business Foundation is the ultimate authority.

This hierarchy ensures that every design decision, at every level, remains connected to the restaurant's operational reality. A designer working on a component color does not need to consult the Business Foundation directly — the constitutional principles in Section 6 provide sufficient guidance. But if a question arises that the constitutional principles cannot answer, the answer lies in the business philosophy. The chain of authority is always traceable back to how the restaurant actually operates.

Business meaning is the ultimate authority over all design decisions. A design decision that is constitutionally consistent but operationally meaningless is still a failure. The constitution exists to serve the business, not the reverse. When business understanding evolves, the constitution may need to evolve with it. But the evolution must be grounded in business reality, not in design preference.

---

## 9.5 Closing Statement

This Design Constitution establishes the principles that will govern every design decision in the JLek POS system for as long as the system exists. It is the reference point for every Design Standard, every implementation, every review, and every evolution of the design.

The Design Standards that follow this document will translate these constitutional principles into concrete, measurable specifications. They will define the colors, the typography, the spacing, the components, and the patterns that realize the principles established here. They will evolve as technology advances and as operational understanding deepens. But the constitutional principles will remain.

A designer creating a new standard must read this document first. An AI generating design content must be verified against this document. An engineer implementing a component must respect the principles defined here. A reviewer evaluating a design decision must measure it against this constitution.

The Business Foundation established how the restaurant operates. This Design Constitution established how the design must serve that operation. The Design Standards will specify the details. Every decision, at every level, must remain consistent with the business philosophy that started it all.

Every future Design Standard,
every implementation,
and every design decision
must derive its legitimacy
from the constitutional principles established in this document,
and ultimately
from the Business Foundation itself.

This Design Constitution is not the conclusion of the Design Series.

It is the constitutional foundation from which every Design Standard derives its authority.