# Playwright E2E Testing - Project Structure & Requirements

## Playwright Configuration

- Provide a Playwright configuration file supporting Chromium, Firefox, and WebKit projects.
- Ensure all browsers run in headed (visible) mode.

## Test Cases

- Include a test to verify that the header is visible.
- Include a test to check that the main list is presented correctly.
- Include a test to confirm that clicking a bikeshop element displays the biker list.

## Project Integration

- Add Playwright as a development dependency in `package.json`.
- Get App URL from the `package.json` file.
- Add an npm script to run Playwright tests.
