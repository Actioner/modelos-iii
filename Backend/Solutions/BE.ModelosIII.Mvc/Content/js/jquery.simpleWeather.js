(function(a){a.extend({simpleWeather:function(c){c=a.extend({zipcode:"76309",location:"",unit:"f",success:function(e){},error:function(e){}},c);var b=new Date();var d="http://query.yahooapis.com/v1/public/yql?format=json&rnd="+b.getFullYear()+b.getMonth()+b.getDay()+b.getHours()+"&diagnostics=true&callback=?&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&q=";if(c.location!==""){d+='select * from weather.forecast where location in (select id from weather.search where query="'+c.location+'") and u="'+c.unit+'"'}else{if(c.zipcode!==""){d+='select * from weather.forecast where location in ("'+c.zipcode+'") and u="'+c.unit+'"'}else{c.error("No location given.");return false}}a.getJSON(d,function(e){if(e!==null&&e.query.results!==null){a.each(e.query.results,function(k,q){if(q.constructor.toString().indexOf("Array")!==-1){q=q[0]}var f=new Date();var o=new Date(f.toDateString()+" "+q.astronomy.sunrise);var h=new Date(f.toDateString()+" "+q.astronomy.sunset);if(f>o&&f<h){var n="d"}else{var n="n"}var g=["N","NNE","NE","ENE","E","ESE","SE","SSE","S","SSW","SW","WSW","W","WNW","NW","NNW","N"];var p=g[Math.round(q.wind.direction/22.5)];if(q.item.condition.temp<80&&q.atmosphere.humidity<40){var l=-42.379+2.04901523*q.item.condition.temp+10.14333127*q.atmosphere.humidity-0.22475541*q.item.condition.temp*q.atmosphere.humidity-6.83783*(Math.pow(10,-3))*(Math.pow(q.item.condition.temp,2))-5.481717*(Math.pow(10,-2))*(Math.pow(q.atmosphere.humidity,2))+1.22874*(Math.pow(10,-3))*(Math.pow(q.item.condition.temp,2))*q.atmosphere.humidity+8.5282*(Math.pow(10,-4))*q.item.condition.temp*(Math.pow(q.atmosphere.humidity,2))-1.99*(Math.pow(10,-6))*(Math.pow(q.item.condition.temp,2))*(Math.pow(q.atmosphere.humidity,2))}else{var l=q.item.condition.temp}if(c.unit==="f"){var m=Math.round((5/9)*(q.item.condition.temp-32))}else{var m=Math.round((9/5)*q.item.condition.temp+32)}var j={title:q.item.title,temp:q.item.condition.temp,tempAlt:m,code:q.item.condition.code,todayCode:q.item.forecast[0].code,units:{temp:q.units.temperature,distance:q.units.distance,pressure:q.units.pressure,speed:q.units.speed},currently:q.item.condition.text,high:q.item.forecast[0].high,low:q.item.forecast[0].low,forecast:q.item.forecast[0].text,wind:{chill:q.wind.chill,direction:p,speed:q.wind.speed},humidity:q.atmosphere.humidity,heatindex:l,pressure:q.atmosphere.pressure,rising:q.atmosphere.rising,visibility:q.atmosphere.visibility,sunrise:q.astronomy.sunrise,sunset:q.astronomy.sunset,description:q.item.description,thumbnail:"http://l.yimg.com/a/i/us/nws/weather/gr/"+q.item.condition.code+n+"s.png",image:"http://l.yimg.com/a/i/us/nws/weather/gr/"+q.item.condition.code+n+".png",tomorrow:{high:q.item.forecast[1].high,low:q.item.forecast[1].low,forecast:q.item.forecast[1].text,code:q.item.forecast[1].code,date:q.item.forecast[1].date,day:q.item.forecast[1].day,image:"http://l.yimg.com/a/i/us/nws/weather/gr/"+q.item.forecast[1].code+"d.png"},city:q.location.city,country:q.location.country,region:q.location.region,updated:q.item.pubDate,link:q.item.link};c.success(j)})}else{if(e.query.results===null){c.error("Invalid location given.")}else{c.error("Weather could not be displayed. Try again.")}}});return this}})})(jQuery);