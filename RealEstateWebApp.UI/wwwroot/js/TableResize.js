(function(){
    'use strict';
    window.TableResize = {};
    
    let _dotNetObject;

    function _subscribe(tableSelector, dotNetObject) {
        const table = document.querySelector(tableSelector);
        if (!table)
            return false;
        
        _dotNetObject = dotNetObject;
        table.querySelector("thead").addEventListener("mousedown", _mouseDown);
        
        return true;
    }
    
    let _currentTarget;
    let _currentHeader;
    let _startX;
    let _startWidth;
    let _currentWidth;
    
    function _mouseDown(e) {
        if (!e.target.classList.contains('resize-handler'))
            return;
        document.addEventListener("mouseup", _mouseUp);
        document.addEventListener("mousemove", _mouseMove);
        
        _currentTarget = e.target;
        _currentHeader = e.target.parentNode;
        _startX = +e.pageX;
        _startWidth = +_currentHeader.offsetWidth;
        _currentWidth = _startWidth;
        
        e.stopPropagation();
        e.preventDefault();
    }
    
    function _mouseUp(e) {
        document.removeEventListener("mousemove", _mouseMove);
        document.removeEventListener("mouseup", _mouseUp);

        e.stopPropagation();
        e.preventDefault();

        const index = Array.prototype.indexOf.call(_currentHeader.parentNode.children, _currentHeader);
        _dotNetObject.invokeMethodAsync("OnSizeChanged", index, _currentWidth);
    }
    
    function _mouseMove(e) {
        const dif = e.pageX - _startX;
        _currentWidth = _startWidth + dif;

        _currentHeader.style.width = _currentWidth + 'px';
    }

    function _unsubscribe(elementSelector){
        //todo add this
    }

    window.TableResize.Subscribe = _subscribe;
    window.TableResize.Unsubscribe = _unsubscribe;
})()