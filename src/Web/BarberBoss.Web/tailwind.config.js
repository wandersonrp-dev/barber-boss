module.exports = {
    content: [        
        './**/*.razor',
        './**/*.cshtml',
        './**/*.razor.cs',
        './wwwroot/**/*.js',
        './wwwroot/**/*.ts',
        './public/**/*.html',
        './src/**/*.{html,razor,cshtml,razor.cs}',
        './node_modules/flowbite/**/*.js'
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

