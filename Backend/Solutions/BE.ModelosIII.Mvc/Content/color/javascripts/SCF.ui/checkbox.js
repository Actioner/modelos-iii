(function(){function c(d){this.element=d}var a=c.prototype;var b=c;SCF.Checkbox=c;a.init=function(){this.bindEvents()};a.bindEvents=function(){var d=this;$(this.element).click(function(){$(this).toggleClass("checked")})}}());