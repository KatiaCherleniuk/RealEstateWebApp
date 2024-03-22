(function(scope){
    scope.Utils = {};
    scope = scope.Utils;
    
    scope.submitForm = function(selector) {
        document.querySelector(selector).submit();     
    }
})(window);