/** @type {import('tailwindcss').Config} */

import { join } from 'path';
import type { Config } from 'tailwindcss';

import { skeleton } from '@skeletonlabs/tw-plugin';

export default {
  content: [
    './src/**/*.{html,js,svelte,ts}',
    join(require.resolve(
      '@skeletonlabs/skeleton'),
      '../**/*.{html,js,svelte,ts}'
    ),
  ],
  safelist: [{
    pattern: /hljs-.+/,
  }],
  theme: {
    extend: {},
  },
  plugins: [
    skeleton({
      themes: {
        preset: [
          { name: "skeleton", enhancements: true },
          { name: "modern", enhancements: true },
          { name: "crimson", enhancements: true },
        ],
      }
    })
  ],
} satisfies Config

