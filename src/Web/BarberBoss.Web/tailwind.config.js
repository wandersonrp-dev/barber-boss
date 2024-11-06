module.exports = {
    content: [
        './**/*.html',
        './**/*.razor',
        './**/*.cshtml',
        './**/*.razor.cs',
        './**/*.js',
        './**/*.ts',
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

