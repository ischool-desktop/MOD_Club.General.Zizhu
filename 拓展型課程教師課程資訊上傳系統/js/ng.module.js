﻿if (!('forEach' in Array.prototype)) {
    Array.prototype.forEach = function (action, that /*opt*/) {
        for (var i = 0, n = this.length; i < n; i++)
            if (i in this)
                action.call(that, this[i], i, this);
    };
}
angular.module('MyApp', []).controller('MyController', ['$scope', '$timeout', function ($scope, $timeout) {

    $scope.connection = dsutil.creatConnection("https://1admin-ap.ischool.com.tw/dsacn/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.teacher", {
        "@": ['Type'],
        Type: 'PassportAccessToken',
        AccessToken: window.accesstoken
    });
    $scope.connection.OnLoginError(function (err) {
        if (err.XMLHttpRequest.responseText.indexOf("User doesn't exist") > 0) {
            alert("账号设定错误。");
        }
        window.gogoLogin();
    });

    $scope.search = "";
    $scope.searchFilter = "";
    $scope.current = {
        schoolYear: "",
        semester: "",
        levelMax: 0,
        grade: "",
        mode: "view",
        shown: null,
        progress: 0,
        selected: [null, null, null]
    };
    $scope.courseList = [];
    $scope.scCount = {};

    $scope.connection.send({
        service: 'GetClubList',
        autoRetry: true,
        result: function (resp, errorInfo, XMLHttpRequest) {
            if (errorInfo == null) {
                $scope.current.schoolYear = resp.SchoolYear;
                $scope.current.semester = resp.Semester;

                [].concat(resp.Item || []).forEach(function (item) {
                    for (var key in { "一年级": 1, "二年级": 1, "三年级": 1, "四年级": 1, "五年级": 1 }) {
                        item.limit[key].all = parseInt(item.limit[key].all, 10);
                        item.limit[key].male = parseInt(item.limit[key].male, 10);
                        item.limit[key].female = parseInt(item.limit[key].female, 10);
                    }
                });

                $scope.courseList = [].concat(resp.Item || []).sort(function (o1, o2) {
                    if (o1.classification != o2.classification) {
                        for (var key in { "文学与艺术": 1, "社会与生活": 1, "运动与生命": 1, "科技与创新": 1, "世界与未来": 1 }) {
                            if (o1.classification.indexOf(key) >= 0) return -1;
                            if (o2.classification.indexOf(key) >= 0) return 1;
                        }
                    }
                    var o1C = 0, o2C = 0;
                    for (var key in { "一年级": 1, "二年级": 1, "三年级": 1, "四年级": 1, "五年级": 1 }) {
                        if (o1.limit[key].all || o1.limit[key].all === 0) o1C++;
                        if (o2.limit[key].all || o2.limit[key].all === 0) o2C++;
                    }
                    if (o1C !== o2C) return o1C - o2C;
                    for (var key in { "一年级": 1, "二年级": 1, "三年级": 1, "四年级": 1, "五年级": 1 }) {
                        if (o1.limit[key].all && o2.limit[key].all) {
                            return o1C - o2C;
                        }
                        if (o1.limit[key].all) return -1;
                        if (o2.limit[key].all) return 1;
                    }
                    return o1.id - o2.id;
                });
            }

            //if (location.href.lastIndexOf('#') >= 0) {
            //    var bookmark = location.href.substr(location.href.lastIndexOf('#') + 1);

            //    var vars = [], hashes = bookmark.split('&');
            //    for (var i = 0; i < hashes.length; i++) {
            //        hash = decodeURIComponent(hashes[i]);
            //        var key = hash.substring(0, hash.indexOf("="));
            //        vars.push(key);
            //        vars[key] = hash.substring(hash.indexOf("=") + 1);
            //    }
            //    if (vars.access_token) {
            //        // var studentConn = dsutil.creatConnection("https://dsa.ischoolcenter.com/1admin/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.student", {
            //        var studentConn = dsutil.creatConnection("https://1admin-ap.ischool.com.tw/dsacn/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.student", {
            //            "@": ['Type'],
            //            Type: 'PassportAccessToken',
            //            AccessToken: vars.access_token
            //        });
            //        studentConn.OnLoginError(function (err) {
            //            if (err.XMLHttpRequest.responseText.indexOf("User doesn't exist") > 0) {
            //                $scope.loginError = "账号设定错误。";
            //                $scope.current.mode = "view";
            //            }
            //            else {
            //                $scope.current.mode = "view";
            //            }
            //            $scope.$apply($scope.reflash);
            //        });
            //        studentConn.ready(function () {
            //            $scope.$apply(function () {
            //                $scope.reload = function () {
            //                    studentConn.send({
            //                        service: 'GetStatus',
            //                        autoRetry: true,
            //                        result: function (resp, errorInfo, XMLHttpRequest) {
            //                            $scope.reflashTick = 8;
            //                            $scope.$apply(function () {
            //                                var gradeString = ['一年级', '二年级', '三年级', '四年级', '五年级'];
            //                                if (parseInt(resp.gradeYear, 10)) {
            //                                    // 2015/9/2 all gradeYear levelMax = 2
            //                                    // if (parseInt(resp.gradeYear, 10) <= 2)
            //                                    //     $scope.current.levelMax = 3;
            //                                    // else
            //                                    //     $scope.current.levelMax = 2;
            //                                    $scope.current.levelMax = 2;
            //                                    $scope.current.gradeYear = parseInt(resp.gradeYear, 10);
            //                                    $scope.current.grade = gradeString[parseInt(resp.gradeYear, 10) - 1];
            //                                }
            //                                $scope.current.gander = resp.gander;
            //                                $scope.current.student = { name: resp.name };
            //                                if (resp.timing.start && resp.timing.end) {
            //                                    $scope.timing = {
            //                                        start: parseInt(resp.timing.start, 10),
            //                                        end: parseInt(resp.timing.end, 10),
            //                                        msg: ($scope.timing && $scope.timing.msg) ? $scope.timing.msg : ""
            //                                    };
            //                                }
            //                                if (!$scope.countTimer)
            //                                    countTime();
            //                                $scope.current.selected = [null, null, null];
            //                                $scope.courseList.forEach(function (course) {
            //                                    if (course.id == resp.selected.phase1)
            //                                        $scope.current.selected[0] = course;
            //                                    if (course.id == resp.selected.phase2)
            //                                        $scope.current.selected[1] = course;
            //                                    if (course.id == resp.selected.phase3)
            //                                        $scope.current.selected[2] = course;
            //                                });
            //                                $scope.scCount = {};
            //                                [].concat(resp.scCount || []).forEach(function (obj) {
            //                                    var key = "_" + obj.ref_club_id + "_" + obj.grade_year + "_" + obj.phase + "_ALL";
            //                                    if (!$scope.scCount[key])
            //                                        $scope.scCount[key] = 0;
            //                                    $scope.scCount[key] += parseInt(obj.count, 10);

            //                                    key = "_" + obj.ref_club_id + "_" + obj.grade_year + "_" + obj.phase + "_" + obj.gender;
            //                                    if (!$scope.scCount[key])
            //                                        $scope.scCount[key] = 0;
            //                                    $scope.scCount[key] += parseInt(obj.count, 10);
            //                                });

            //                                if ($scope.current.selected[0] && $scope.current.selected[0].fullPhase == 'true') {
            //                                    $scope.current.progress = 100;
            //                                }
            //                                else {
            //                                    var scount = 0;
            //                                    for (var i = 0; i < $scope.current.levelMax; i++) {
            //                                        if ($scope.current.selected[i])
            //                                            scount++;
            //                                    }

            //                                    switch ($scope.current.levelMax) {
            //                                        case 1:
            //                                            if (scount == 0)
            //                                                $scope.current.progress = 0;
            //                                            if (scount == 1)
            //                                                $scope.current.progress = 100;
            //                                            break;
            //                                        case 2:
            //                                            if (scount == 0)
            //                                                $scope.current.progress = 0;
            //                                            if (scount == 1)
            //                                                $scope.current.progress = 50;
            //                                            if (scount == 2)
            //                                                $scope.current.progress = 100;
            //                                            break;
            //                                        case 3:
            //                                            if (scount == 0)
            //                                                $scope.current.progress = 0;
            //                                            if (scount == 1)
            //                                                $scope.current.progress = 33;
            //                                            if (scount == 2)
            //                                                $scope.current.progress = 66;
            //                                            if (scount == 3)
            //                                                $scope.current.progress = 100;
            //                                            break;
            //                                    }
            //                                }
            //                            });
            //                        }
            //                    });
            //                }
            //                $scope.reload();


            //                $scope.selectCourse = function (course, phase) {
            //                    if ($scope.current.selected[phase - 1] && $scope.current.selected[phase - 1] == course) {
            //                        course.showCancel = phase;
            //                        if (course.showCancelTimer) {
            //                            $timeout.cancel(course.showCancelTimer);
            //                        }
            //                        course.showCancelTimer = $timeout(function () {
            //                            delete course.showCancel;
            //                            delete course.showCancelTimer;
            //                        }, 5000);
            //                    }
            //                    else {
            //                        if ($scope.isloading) return;
            //                        $scope.isloading = true;
            //                        studentConn.send({
            //                            service: 'JoinClub',
            //                            autoRetry: true,
            //                            body: { id: course.id, phase: phase },
            //                            result: function (resp, errorInfo, XMLHttpRequest) {
            //                                delete $scope.isloading;
            //                                if (resp.error) {
            //                                    $scope.fullMsg = resp.error;
            //                                    course.showFull = true;
            //                                    if (course.showFullTimer) {
            //                                        $timeout.cancel(course.showFullTimer);
            //                                    }
            //                                    course.showFullTimer = $timeout(function () {
            //                                        delete course.showFull;
            //                                        delete course.showFullTimer;
            //                                    }, 2000);
            //                                }
            //                                else {
            //                                    $scope.reload();
            //                                }
            //                            }
            //                        });
            //                    }
            //                }

            //                $scope.removeCourse = function (course, phase) {
            //                    if ($scope.isloading) return;
            //                    $scope.isloading = true;
            //                    delete course.showCancel;
            //                    delete course.showCancelTimer;
            //                    studentConn.send({
            //                        service: 'LeaveClub',
            //                        autoRetry: true,
            //                        body: { id: course.id, phase: phase },
            //                        result: function (resp, errorInfo, XMLHttpRequest) {
            //                            delete $scope.isloading;
            //                            if (resp.error) {
            //                                $scope.fullMsg = resp.error;
            //                                course.showFull = true;
            //                                if (course.showFullTimer) {
            //                                    $timeout.cancel(course.showFullTimer);
            //                                }
            //                                course.showFullTimer = $timeout(function () {
            //                                    delete course.showFull;
            //                                    delete course.showFullTimer;
            //                                }, 2000);
            //                            }
            //                            else {
            //                                $scope.reload();
            //                            }
            //                        }
            //                    });
            //                }
            //            });
            //        });
            //    }
            //    else {
            //        $scope.current.mode = "view";
            //    }
            //}
            //else {
            //    $scope.current.mode = "view";
            //}


            $scope.$apply($scope.reflash);
        }
    });





    $scope.parseInt = window.parseInt;


    $scope.srcList = [
		"http://pic1.ooopic.com/uploadfilepic/yuanwenjian/2009-10-17/OOOPIC_760996499_20091017bc8dfbd1a1a24095.jpg",
		"http://upaiyun.gogo.com.cn//Uploads/20141222/1419230745_33129.jpg!super",
		"http://img9.3lian.com/c1/vector/20/01/13.jpg",
		"http://www.itingwa.com/file/img/2014-02/20140203102351-NTMxODQy_500x361.jpg",
		"http://pic.shejiben.com/wzcourse/94/28f952df26cc50fc11828e2aeb76bd6f.jpg",
		"http://img.taopic.com/uploads/allimg/110717/1616-110GGG52875.jpg",
		"http://www.chinahexie.org.cn/uploads/allimg/110525/8_110525134028_1.jpg"
    ];
    $scope.setSearch = function () {
        $scope.searchFilter = $scope.search;
        $scope.reflash();
    }
    $scope.unsetSearch = function () {
        $scope.searchFilter = $scope.search = "";
        $scope.reflash();
    }
    $scope.reflash = function () {
        var courseWithClassification = {};
        var classificationList = [];
        var resetShown = true;
        var first = null;
        $scope.courseList.forEach(function (course) {
            if ($scope.searchFilter && JSON.stringify(course).indexOf($scope.searchFilter) == -1)
                return;
            if (course.limit['一年级'].all ||
                course.limit['二年级'].all ||
                course.limit['三年级'].all ||
                course.limit['四年级'].all ||
                course.limit['五年级'].all ||
                course.limit['一年级'].all == 0 ||
                course.limit['二年级'].all == 0 ||
                course.limit['三年级'].all == 0 ||
                course.limit['四年级'].all == 0 ||
                course.limit['五年级'].all == 0
                ) {
                if ((($scope.current.mode == "select" || $scope.current.mode == "end") && (course.limit[$scope.current.grade].all || course.limit[$scope.current.grade].all == 0)) ||
                    ($scope.current.mode == "view")
                    ) {
                    if (!courseWithClassification[course.classification]) {
                        classificationList.push(course.classification);
                        courseWithClassification[course.classification] = [];
                    }
                    courseWithClassification[course.classification].push(course);

                    if ($scope.current.shown == course) {
                        resetShown = false;
                    }
                    if (first == null)
                        first = course;
                }
            }
        });
        if (resetShown) {
            $scope.current.shown = first;
        }
        $scope.classificationList = classificationList;
        $scope.courseWithClassification = courseWithClassification;
    }
    $scope.getClassificationStyle = function (classification) {
        var styles = {
            "文学与艺术": 'orange_left',
            "社会与生活": 'yellow_left',
            "运动与生命": 'green_left',
            "科技与创新": 'cyan_left',
            "世界与未来": 'blue_left'
        };
        for (var key in styles) {
            if (classification.indexOf(key) >= 0)
                return styles[key];
        }
    }
    $scope.selectDisable = function (course, phase) {
        if ($scope.current.mode == 'end')
            return true;
        if ($scope.current.selected[0] && $scope.current.selected[0].fullPhase == 'true' && $scope.current.selected[0] != course)
            return true;
        for (var i = 0; i < $scope.current.levelMax; i++) {
            if (i == phase - 1) {
                if ($scope.current.selected[i] && $scope.current.selected[i] != course)
                    return true;
            }
            else {
                if ($scope.current.selected[i] && $scope.current.selected[i].classification == course.classification) {
                    return true;
                }
                if ($scope.current.selected[i] && course.fullPhase == 'true') {
                    return true;
                }
            }
        }
        return false;
    }
    $scope.isFull = function (course, phase) {
        if ($scope.current.mode == 'select') {
            if ($scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_ALL"] &&
                $scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_ALL"] >= course.limit[$scope.current.grade].all)
                return true;
            if ($scope.current.gander == "1" &&
                $scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_" + $scope.current.gander] &&
                $scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_" + $scope.current.gander] >= course.limit[$scope.current.grade].male)
                return true;
            if ($scope.current.gander == "0" &&
                $scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_" + $scope.current.gander] &&
                $scope.scCount["_" + course.id + "_" + $scope.current.gradeYear + "_" + phase + "_" + $scope.current.gander] >= course.limit[$scope.current.grade].female)
                return true;
        }
    }
    $scope.rowClass = function (course) {
        for (var i = 0; i < $scope.current.levelMax; i++) {
            if ($scope.current.selected[i] == course)
                return "success";
        }
        if ($scope.current.shown == course)
            return "warning";
        return "";
    };
    $scope.login = window.gogoLogin;
    $scope.logout = window.gogoLogin;

    $scope.editDescription = function () {
        $scope.editDes = $scope.current.shown.description;
    }
    $scope.submitDescription = function (val) {
        $scope.current.shown.description = val;
        delete $scope.editDes;
        var request = {
            Club: {
                ID: $scope.current.shown.id
            }
        };
        request.Club['Description'] = $scope.current.shown.description;
        $scope.connection.send({
            service: 'SetClubInfo',
            autoRetry: true,
            body: { Request: request },
            result: function (resp, errorInfo, XMLHttpRequest) {
                if (errorInfo) {
                    alert("上傳失敗，請重新操作。");
                }
            }
        });
    }
    $scope.cancelDescription = function () {
        delete $scope.editDes;
    }

    var files = $('<input type="file" class="hide">');
    $('body').append(files);
    $scope.changPhoto = function (index) {

        files.val('').trigger('click');
        files.change(function (evt) {
            if (evt.target == undefined ||
                    evt.target.files == undefined ||
                    evt.target.files.length == 0) {
                alert("您的瀏覽器並未支援讀取檔案功能，請更新您的瀏覽器，謝謝!\n\n建議瀏覽器：Chrome 10+, IE 10+, Firefox 10+");
                return;
            }
            var file = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = (function (theFile) {
                return function (e) {
                    var image = new Image();
                    image.src = e.target.result;
                    image.onload = function () {
                        var finalFile = "";

                        var maxWidth = 640, maxHeight = 480, imageHeight = image.height, imageWidth = image.width;

                        if (imageHeight > maxHeight) {
                            imageWidth *= maxHeight / imageHeight;
                            imageHeight = maxHeight;
                        }
                        if (imageWidth > maxWidth) {
                            imageHeight *= maxWidth / imageWidth;
                            imageWidth = maxWidth;
                        }

                        var canvas = document.createElement('canvas');
                        canvas.width = imageWidth;
                        canvas.height = imageHeight;

                        var ctx = canvas.getContext("2d");
                        ctx.drawImage(this, 0, 0, imageWidth, imageHeight);

                        // The resized file ready for upload
                        finalFile = canvas.toDataURL(file.type);


                        finalFile = finalFile.substr(finalFile.indexOf(",") + 1);
                        $scope.$apply(function () {
                            $scope.current.shown['photo' + index] = finalFile;
                        });
                        var request = {
                            Club: {
                                ID: $scope.current.shown.id
                            }
                        };
                        request.Club['Photo' + index] = finalFile;
                        $scope.connection.send({
                            service: 'SetClubInfo',
                            autoRetry: true,
                            body: { Request: request },
                            result: function (resp, errorInfo, XMLHttpRequest) {
                                if (errorInfo) {
                                    alert("上傳失敗，請重新操作。");
                                }
                            }
                        });
                    }
                };
            })(file);
            reader.readAsDataURL(file);
        });
    }
}
]);