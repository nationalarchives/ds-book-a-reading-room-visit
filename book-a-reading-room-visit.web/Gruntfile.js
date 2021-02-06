module.exports = function (grunt) {
    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        sass: {
            options: {
                sourcemap: false
            },
            dist: {
                files: {
                    'wwwroot/css/base-sass.css': 'wwwroot/css/sass/base-sass.scss'
                }
            }
        },
        cssmin: {
            options: {
                sourceMap: true
            },
            target: {
                files: {
                    'wwwroot/css/compiled-css/base-sass.min.css': ['wwwroot/css/base-sass.css']
                }
            }
        },
        watch: {
            css: {
                files: 'wwwroot/css/sass/*.scss',
                tasks: ['sass', 'cssmin']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');

    //Default task(s).
    grunt.registerTask('default', [
        'sass',
        'cssmin',
        'watch'
    ]);
};