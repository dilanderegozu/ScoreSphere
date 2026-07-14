---
name: ScoreSphere Management
colors:
  surface: '#0d141d'
  surface-dim: '#0d141d'
  surface-bright: '#333a44'
  surface-container-lowest: '#080f17'
  surface-container-low: '#151c25'
  surface-container: '#192029'
  surface-container-high: '#232a34'
  surface-container-highest: '#2e353f'
  on-surface: '#dce3f0'
  on-surface-variant: '#c6c5d0'
  inverse-surface: '#dce3f0'
  inverse-on-surface: '#2a313b'
  outline: '#90909a'
  outline-variant: '#45464f'
  surface-tint: '#b7c4ff'
  primary: '#dde1ff'
  on-primary: '#1f2d5e'
  primary-container: '#b7c4ff'
  on-primary-container: '#435083'
  inverse-primary: '#4f5c90'
  secondary: '#bdc7d9'
  on-secondary: '#27313f'
  secondary-container: '#404a59'
  on-secondary-container: '#afb9cb'
  tertiary: '#d9e3f7'
  on-tertiary: '#273140'
  tertiary-container: '#bdc7db'
  on-tertiary-container: '#495364'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#dce1ff'
  primary-fixed-dim: '#b7c4ff'
  on-primary-fixed: '#071749'
  on-primary-fixed-variant: '#374476'
  secondary-fixed: '#d9e3f6'
  secondary-fixed-dim: '#bdc7d9'
  on-secondary-fixed: '#121c2a'
  on-secondary-fixed-variant: '#3d4756'
  tertiary-fixed: '#d9e3f7'
  tertiary-fixed-dim: '#bdc7db'
  on-tertiary-fixed: '#121c2a'
  on-tertiary-fixed-variant: '#3d4757'
  background: '#0d141d'
  on-background: '#dce3f0'
  surface-variant: '#2e353f'
typography:
  display-lg:
    fontFamily: Montserrat
    fontSize: 48px
    fontWeight: '700'
    lineHeight: 56px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Montserrat
    fontSize: 32px
    fontWeight: '600'
    lineHeight: 40px
    letterSpacing: -0.01em
  headline-md:
    fontFamily: Montserrat
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  title-lg:
    fontFamily: Montserrat
    fontSize: 20px
    fontWeight: '600'
    lineHeight: 28px
  body-lg:
    fontFamily: Montserrat
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  body-md:
    fontFamily: Montserrat
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-lg:
    fontFamily: Montserrat
    fontSize: 14px
    fontWeight: '600'
    lineHeight: 20px
    letterSpacing: 0.1px
  label-sm:
    fontFamily: Montserrat
    fontSize: 11px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.5px
  headline-lg-mobile:
    fontFamily: Montserrat
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  unit: 4px
  gutter: 24px
  margin-mobile: 16px
  margin-desktop: 32px
  container-max: 1440px
---

## Brand & Style
The design system for this premium SaaS dashboard is built upon a foundation of "Technical Elegance." It targets data-driven professionals who require high-density information presented with clarity and prestige. 

The aesthetic is a sophisticated blend of **Corporate Modern** and **Material Design 3**, optimized for a dark-mode environment. It prioritizes depth through tonal layering and subtle elevation rather than flat surfaces. The interface should feel expansive, reliable, and high-end, utilizing the primary accent to draw attention to critical actions and data points while maintaining a calm, focused workspace.

## Colors
The palette is centered on a high-end dark theme. The background utilizes a deep, matte charcoal (`#111827`), providing a low-strain canvas for long-term usage. 

- **Primary Accent (`#B7C4FF`):** A soft, periwinkle blue used for active states, primary call-to-actions, and data highlights.
- **Surface Hierarchy:** UI elements use a stepped grayscale. `Surface` (`#1F2937`) is used for primary cards, while `Surface Container` (`#111827`) is used for the main layout background.
- **Semantic Colors:** Success, Warning, and Error states should be desaturated to maintain the premium feel, ensuring they don't clash with the primary accent.

## Typography
Montserrat is the exclusive typeface, providing a modern, geometric structure that feels both friendly and professional. 

- **Hierarchy:** Use `Bold` (700) for large display numbers and `SemiBold` (600) for section headers. 
- **Readability:** Body text uses `Regular` (400) with generous line heights to ensure legibility against the dark background.
- **Micro-copy:** Labels and captions use `Medium` (500) weights with slightly increased letter spacing to distinguish them from standard body copy.

## Layout & Spacing
The design system employs a **Fluid Grid** model with a 12-column structure for desktop. 

- **Grid:** Use 24px gutters to allow data-heavy components enough "breathing room."
- **Sidebars:** The navigation sidebar is fixed at 280px (expanded) or 80px (collapsed).
- **Rhythm:** All vertical and horizontal spacing must follow a 4px base unit. Component internal padding should default to 16px or 20px to match the roundedness of the containers.
- **Breakpoints:**
  - Mobile: < 600px (1-column, 16px margins)
  - Tablet: 600px - 1024px (6-column, 24px margins)
  - Desktop: > 1024px (12-column, 32px margins)

## Elevation & Depth
In alignment with Material Design 3, depth is communicated through **Tonal Layers** and soft shadows.

- **Level 0 (Background):** `#111827` – The base canvas.
- **Level 1 (Cards/Sidebar):** `#1F2937` – Primary surface for content.
- **Level 2 (Modals/Popovers):** `#374151` – High elevation elements.
- **Shadows:** Use extremely soft, large-radius shadows (Blur: 20px-40px, Opacity: 25%) with a slight blue tint (`#000000`) to create a sense of floating without looking "muddy" on the dark background. 
- **Interactions:** On hover, cards should lift slightly (translate -2px) and shadows should expand in spread, not opacity.

## Shapes
The shape language is consistently "Rounded Sixteen." This provides a soft, premium feel that counters the technical nature of a dashboard.

- **Standard Containers:** Use 16px border-radius for all cards, sections, and primary containers.
- **Interactive Elements:** Buttons and input fields use 12px-16px radius to maintain harmony.
- **Large Components:** Modals and large empty-state containers may scale up to 24px for a more distinct, high-end look.

## Components
Consistent execution of these components ensures the "ScoreSphere" interface remains cohesive:

- **Buttons:**
  - *Primary:* Solid `#B7C4FF` background with black text. 16px radius.
  - *Secondary:* Ghost style with a 1px border of `#374151` and primary color text.
- **Cards:**
  - Solid `#1F2937` background, 16px radius, subtle 1px top-border (`#FFFFFF10`) to simulate a "light catch" on the edge.
- **Input Fields:**
  - Background: `#111827`. Border: 1px solid `#374151`. 12px radius. On focus, the border transitions to `#B7C4FF`.
- **Chips/Badges:**
  - Small, 8px radius or fully rounded. Use a low-opacity version of the primary color (e.g., `#B7C4FF15`) for backgrounds to keep them subtle.
- **Lists:**
  - Use horizontal dividers with `#FFFFFF08` opacity. Items should have a subtle hover state using `#FFFFFF05`.
- **Data Visualizations:**
  - Charts should exclusively use the primary accent and complementary cool tones (teals, soft purples) to ensure visual harmony with the dashboard theme.