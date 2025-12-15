# Bikeshop App - Project Structure & Requirements

## 1. Project Setup

- Create a new directory: `bikeshop-app` under `src`.

## 2. HTML Template

- Prepare an HTML file with a basic template layout.
- The HTML should include:
  - A container for the bikeshop list.
  - A section to display bikes available in a selected bikeshop.

## 3. JavaScript Functionality

- Link a JS file to the HTML.
- On page load, fetch bikeshops (Get data from #file:BikeShopController.cs).
- Set Base API URL according to #file:launchSettings.json
- Display bikeshops in a simple list.
  - Each list item should show:
    - Bikeshop name
    - Description
    - A colorful tag with the category
- On clicking a bikeshop item:
  - Show a list of bikes available in that bikeshop inside the Bikeshop element.

## 4. CSS Styling

- Create a CSS file with base styles.
- Use a color scheme based on light blue, white, and black.

## 5. Local Hosting

- Prepare an `npm` `package.json` to host the app locally using the `serve` package.
- Host the app locally on port `3010`.

## 6. Additional Setup

- Run `npx playwright install` (for end-to-end testing setup, if needed).
