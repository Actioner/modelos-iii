(function(b){var a={init:function(c){return this.each(function(){var d=new Gauge(b(this)[0],c);b(this).data("gauge",d)})},setValue:function(c){return this.each(function(){var d=b(this).data("gauge");if(d!=null){d.setValue(c)}})},draw:function(){return this.each(function(){var c=b(this).data("gauge");if(c!=null){c.draw()}})}};b.fn.gauge=function(c){if(a[c]){return a[c].apply(this,Array.prototype.slice.call(arguments,1))}else{if(typeof c==="object"||!c){return a.init.apply(this,arguments)}else{b.error("Method "+c+" does not exist on jQuery.gauge")}}}})(jQuery);