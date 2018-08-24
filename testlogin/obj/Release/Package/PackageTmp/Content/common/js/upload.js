
	var accessid = '6MKOqxGiGU4AUk44';
	var accesskey = 'ufu7nS8kS59awNihtjSonMETLI0KLy';
	var host = 'http://cn-bookse-yunpai.oss-cn-hangzhou.aliyuncs.com';

	//XmlHttpRequest对象
	function createXmlHttpRequest() {
		if (window.ActiveXObject) { //如果是IE浏览器
			return new ActiveXObject("Microsoft.XMLHTTP");
		} else if (window.XMLHttpRequest) { //非IE浏览器
			return new XMLHttpRequest();
		}
	}

	var POLICY_JSON = {
		"expiration": "2020-01-01T12:00:00.000Z", //设置该Policy的失效时间，超过这个失效时间之后，就没有办法通过这个policy上传文件了
		"conditions": [
			["content-length-range", 0, 1048576000] // 设置上传文件的大小限制
		]
	};
	var policyBase64 = Base64.encode(JSON.stringify(POLICY_JSON));
	var signature = b64_hmac_sha1(accesskey, policyBase64);

	function uploadProgress() {

	}

	function uploadComplete() {
		window.wxc.xcConfirm("上传成功!","success");
	}

	function uploadFailed() {
		window.wxc.xcConfirm("上传失败!","error");
	}

	function uploadCanceled() {
		window.wxc.xcConfirm("已中止上传!","warning");
	}

	//上传母包
	function uploadOssFile(file, file_url,options) {

		var fd = new FormData();
		fd.append('key', file_url);
		fd.append('Content-Type', file.type);
		fd.append('OSSAccessKeyId', accessid);
		fd.append('policy', policyBase64);
		fd.append('signature', signature);
		fd.append("file", file);

		var config = $.extend({
			//事件
			uploadProgress: uploadProgress,
			uploadComplete: uploadComplete,
			uploadFailed: uploadFailed,
			uploadCanceled:uploadCanceled
		},  options);

		var xhr = createXmlHttpRequest();
		xhr.upload.addEventListener("progress", config.uploadProgress, false);
		xhr.addEventListener("load", config.uploadComplete, false);
		xhr.addEventListener("error", config.uploadFailed, false);
		xhr.addEventListener("abort", config.uploadCanceled, false);
		xhr.upload.addEventListener('error',function(e){
			$(".pack-modal").mark("hide");
			$(".package-modal").mark("hide");
		},false);
		xhr.upload.addEventListener('loadend',function(e){
			$(".pack-modal").mark("hide");
			$(".package-modal").mark("hide");
		},false);
		xhr.upload.addEventListener('loadstart',function(e){
			$(".pack-modal").mark("show");
			$(".package-modal").mark("show");
		},false);
		xhr.open('POST', host, true); //MUST BE LAST LINE BEFORE YOU SEND
		xhr.send(fd);
	}

	//建立一個可存取到該file的url
	function getObjectURL(file) {
		var url = null;
		if (window.createObjectURL != undefined) { // basic
			url = window.createObjectURL(file);
		} else if (window.URL != undefined) { // mozilla(firefox)
			url = window.URL.createObjectURL(file);
		} else if (window.webkitURL != undefined) { // webkit or chrome
			url = window.webkitURL.createObjectURL(file);
		}
		return url;
	}



