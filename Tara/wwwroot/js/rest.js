let rest = {};

rest.get = (url, params, callback = null) => {
    try {
        // Appened params to url.
        if (params)
            url = url + "?" + new URLSearchParams(params);
        // Send get request.
        fetch(url, {
            method: 'GET',
            redirect: 'follow'
        }).then(response => response.json())
            .then(result => callback(true, result))
            .catch(error => {
                console.log('error', error)
                if (callback != null)
                    return callback(false, error)
            });
    } catch (e) {
        if (callback != null) {
            console.log(e)
            return callback(false, e)
        }
    }
}
rest.getAsync = async (url, params, callback = null) => {
    let requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };
    if (params)
        url = url + "?" + new URLSearchParams(params)
    let request = await fetch(url, requestOptions);
    if (request.status != 200)
        return callback(false, '');
    try {
        let response = await request.json();
        if (callback != null)
            return callback(request.ok, response);
    } catch (e) {
        if (callback != null) {
            return callback(true, null)
        }
    }
}

rest.post = (url, body, callback = null) => {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: JSON.stringify(body),
            redirect: 'follow'
        };
        fetch(url, requestOptions)
            .then(response => response.json())
            .then(result => callback(true, result))
            .catch(error => {
                console.log('error', error);
                return callback(false, error);
            });
    } catch (e) {
        console.log('error', error);
        return callback(false, error);
    }
}
rest.postAsync = async (url, params, body, callback = null) => {
    var headers = new Headers();
    headers.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'POST',
        redirect: 'follow'
    };
    if (params)
        url = url + "?" + new URLSearchParams(params)
    if (body) {
        requestOptions.body = JSON.stringify(body);
    }
    requestOptions.headers = headers;
    var request = await fetch(url, requestOptions);
    var response = await request.json();
    if (callback != null)
        return callback(true, response);

    if (request.status != 200)
        return callback(false, '');
 

    return callback(request.ok, response);
}