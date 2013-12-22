# GitHub client for Windows Phone 8

This little project is a read-only GitHub client for Windows Phone 8.
It's still under development, but when I found the time to finish it,
you can get it from the app store as well.

## What can this do?
* Viewing your repositories
* Browsing and viewing source code files
* Viewing diffs in your commits
* Checking your commit history

## Why not use OctoKit.NET?

This project was meant to be fun, and trying to set up a library to
support WP8 is not fun. So I decided that I might as well write the few 
API calls needed for this app .

## Where are the unit tests?

Sadly there's no decent support for unit tests in WP8. Running tests
on the device feels awkward for me, and I don't intend to buy Windows 8 Pro
just to run them in a simulator.

I believe that unit tests should be run very often, without interrupting the
workflow. This is not yet supported on WP8.