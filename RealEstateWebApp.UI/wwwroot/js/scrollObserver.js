(function(){
    'use strict';
    window.ScrollObserver = {};
    
    const offset = 80;
    const timeDelayMs = 100;
    const handlerDictionary = {};

    function _subscribe(elementSelector, dotNetObject) {
        const element = document.querySelector(elementSelector);
        if (!element)
            return false;
        let triggerTime = 0;
        handlerDictionary[elementSelector] = function() {
            if (this.scrollHeight - this.scrollTop - this.clientHeight < offset) {
                let d = + new Date();
                if (d - triggerTime <= timeDelayMs)
                    return;
                triggerTime = d;
                dotNetObject.invokeMethodAsync("OnScrollTriggered", elementSelector);
            }
        };
        element.addEventListener('scroll', handlerDictionary[elementSelector]);
        return true;
    }
    
    function _unsubscribe(elementSelector){
        const element = document.querySelector(elementSelector);
        if (!element)
            return;
        element.removeEventListener('scroll', handlerDictionary[elementSelector]);
        delete handlerDictionary[elementSelector];
    }
    

    window.ScrollObserver.Subscribe = _subscribe;
    window.ScrollObserver.Unsubscribe = _unsubscribe;
})()