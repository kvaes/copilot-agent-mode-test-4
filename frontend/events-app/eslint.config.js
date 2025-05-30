import pluginVue from 'eslint-plugin-vue'

export default [
  // Use the flat config for Vue 3 essential rules
  ...pluginVue.configs['flat/essential'],
  
  // JavaScript files configuration
  {
    files: ['**/*.js', '**/*.jsx', '**/*.cjs', '**/*.mjs'],
    languageOptions: {
      ecmaVersion: 'latest',
      sourceType: 'module'
    }
  },
  
  // Global ignores
  {
    ignores: [
      '**/node_modules/**',
      '**/dist/**',
      '**/build/**',
      '**/.env*',
      '**/logs/**',
      '**/*.log',
      '**/coverage/**',
      '**/bin/**',
      '**/obj/**',
      '**/.vscode/**',
      '**/.idea/**',
      '**/tmp/**',
      '**/temp/**'
    ]
  }
]