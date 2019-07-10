mergeInto(LibraryManager.library, {

    LoadResourceNative: function (location) {
        var locationString = Pointer_stringify(location);
        var request = new XMLHttpRequest();
        request.open('GET', locationString, false);
        request.overrideMimeType('text\/plain; charset=x-user-defined');
        request.send(null);
        
        var response = request.responseText;
        
        var pointer = _malloc(response.length);
        var dataHeap = new Uint8Array(HEAPU8.buffer, pointer, response.length);
        for (var i=0; i < response.length; i++) {
            dataHeap[i] = response.charCodeAt(i) & 0xff;
        }
        
        window.lastLoadedResourceLength = response.length;
        return pointer;
    },
    
    GetLastLoadedResourceLengthNative : function () {
        return window.lastLoadedResourceLength;
    }

});