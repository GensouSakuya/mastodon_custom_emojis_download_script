var instanceName = '实例名';
var custom_emojis;
fetch("https://" + instanceName + "/api/v1/custom_emojis").then(function (res) {
    return res.json()
}).then(function (json) {
    custom_emojis = json;
    custom_emojis.forEach(function (itm) {
        fetch(itm.url).then(res => res.blob().then(blob => {
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(blob);
            var filename = itm.shortcode + '.png';
            a.href = url;
            a.download = filename;
            a.click();
            window.URL.revokeObjectURL(url);
        }))
    })
})
