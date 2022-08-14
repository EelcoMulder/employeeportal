# Use a single project for the UI

* Status: proposed
* Date: 2022-08-14

## Context and Problem Statement

Vertical slices are not out-of-the-box supported by ASPNet.Core (and probaly most other web frameworks) We need to tie the slices together.

## Considered Options

* Put UI also in seperate project, this will require more customization and configuration
* Use one UI project, we can go faster and discover gradually how to move more stuff to the correct slice.

## Decision Outcome

Chosen option: "Use one UI project, we can go faster and discover gradually how to move more stuff to the correct slice.", because we want fast feefback in this fase
