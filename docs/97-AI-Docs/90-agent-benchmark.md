# AI Agent Benchmark

## Purpose

This benchmark evaluates engineering behavior rather than model intelligence. Every benchmark must be reproducible, and results must be evidence-based. Benchmarks should focus on verifiable implementation outcomes rather than abstract model capabilities.

---

## Evaluation Principles

- **Evidence before claims**: All assertions must be supported by verifiable file content or tool outputs.
- **Workflow compliance**: Must follow AI Engineering Standard and AI Workflow exactly.
- **Repository grounding**: All claims must be validated against actual implementation files.
- **Human approval**: Changes require explicit human validation before implementation.
- **Minimal changes**: Solutions must avoid unnecessary modifications to existing code.
- **Verification before completion**: Final results must be verified through tool outputs or file content.

---

## Benchmark Categories

### Onboarding

- Completion of mandatory onboarding documents
- Adherence to AI Engineering Standard

### Documentation Following

- Compliance with repository documentation
- Consistency with existing AI documentation

### Tool Calling

- Correct use of tool commands
- Proper parameter formatting
- Handling of tool execution failures

### File Operations

- **Read**: Accurate file content retrieval
- **Create**: Proper file structure and content
- **Edit**: Precise modifications with minimal changes
- **Rename**: Correct file path updates

### Build

- Successful compilation of projects
- Handling of build errors

### Build Recovery

- Resolution of build failures
- Root cause identification

### Runtime Recovery

- Handling of runtime exceptions
- Validation of recovery procedures

### Repository Navigation

- Correct use of file paths
- Understanding of directory structure

### Existing Implementation Review

- Accurate analysis of current code
- Identification of DDD principles

### Reuse Before Creation

- Preference for existing implementations
- Avoidance of redundant code creation

### Human Approval

- Proper use of approval workflows
- Waiting for validation before changes

### Engineering Workflow

- Adherence to task progression
- Proper use of task_progress tracking

### Self Review

- Verification of implementation
- Documentation of findings

### Documentation Impact

- Proper updates to repository docs
- No unnecessary documentation changes

### Architecture Review

- Compliance with domain models
- Proper use of DDD principles

### Code Review

- Validation of implementation quality
- Identification of risks and improvements

---

## Scoring

| Score       | Criteria                                       |
| ----------- | ---------------------------------------------- |
| **PASS**    | All requirements met with verified evidence    |
| **PARTIAL** | Some requirements met with incomplete evidence |
| **FAIL**    | Requirements not met or evidence contradicted  |

---

## Benchmark Report Template

### Verified Facts

- List of verified file contents or tool outputs

### Evidence

- Supporting data from file reads or tool executions

### Findings

- Analysis of implementation quality

### Risks

- Identified technical risks or issues

### Recommendations

- Proposed improvements or changes

### Final Result

- Overall score (PASS/PARTIAL/FAIL)

---

## Benchmark History

| Date | Model | Version | Environment | Result | Notes |
| ---- | ----- | ------- | ----------- | ------ | ----- |
|      |       |         |             |        |       |

---

## Initial Benchmark Result

**Model**: Qwen 3 14B  
**Verified Evidence**:

- Document creation: `docs/97-AI-Docs/90-agent-benchmark.md` was successfully created with the required structure.
- Build process: `dotnet build JLek.POS.sln` completed with one warning (CS8618) but no errors.
- File operations: `write_to_file` successfully updated `src/JLek.POS.Domain/Tests/LoopAgentTest.cs` with valid content.  
  **Result**: PASS (All verified evidence aligns with task requirements)

## Environment Limitations

The following benchmark results were affected by the execution environment rather than verified model capability.

- Repository-wide search visibility
- Workspace file discovery
- Recursive repository indexing

These limitations should not be interpreted as model deficiencies unless reproduced under a fully verified environment.

### Verified Observations

- read_file successfully accessed Entity.cs.
- search_files did not return the same file.
- Directory traversal successfully located Common/.
- Workspace navigation worked correctly.

### Conclusion

Repository navigation is functioning.

Search behavior appears to depend on the underlying tool implementation rather than the model itself.

Repository-wide search benchmarks should distinguish between

- Directory traversal
- Exact path access
- Filename search
- Content search

These capabilities should not be evaluated as a single metric.
---------
### Search Capability

Search should be benchmarked separately:

- Filename Search
- Path Search
- Full-text Search
- Symbol Search

These are different capabilities and must not be evaluated as a single metric.
-----------
## Evidence Consistency

Tests whether the AI maintains consistency across multiple tool invocations.

The AI must not invalidate previously verified evidence without explicit re-verification.

Conflicting tool outputs must be reported rather than silently replacing verified facts.