﻿if (!('forEach' in Array.prototype)) {
    Array.prototype.forEach = function (action, that /*opt*/) {
        for (var i = 0, n = this.length; i < n; i++)
            if (i in this)
                action.call(that, this[i], i, this);
    };
}
angular.module('MyApp', []).controller('MyController', ['$scope', '$timeout', function ($scope, $timeout) {
    var clientID = "200b644d9a6360d9ac131aa5810e4408";
    var application = "zzxx.mhedu.sh.cn";
    var dn = "zzxx.mhedu.sh.cn";
    var redirect_uri = location.href.lastIndexOf('#') >= 0 ? location.href.substr(0, location.href.lastIndexOf('#')) : location.href;

    $scope.search = "";
    $scope.searchFilter = "";
    $scope.current = {
        schoolYear: "",
        semester: "",
        levelMax: 0,
        grade: "",
        //mode: "eval",
        //mode: "view",
        //mode: "end",
        mode: "",
        shown: null,
        progress: 0,
        selected: [null, null, null],
        assessment: {},
        mate: {}
    };
    $scope.courseList = [];



    if (location.href.lastIndexOf('#') >= 0) {
        var bookmark = location.href.substr(location.href.lastIndexOf('#') + 1);

        var vars = [], hashes = bookmark.split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = decodeURIComponent(hashes[i]);
            var key = hash.substring(0, hash.indexOf("="));
            vars.push(key);
            vars[key] = hash.substring(hash.indexOf("=") + 1);
        }
        if (vars.access_token) {
            $scope.loginProgress = true;
            var studentConn = dsutil.creatConnection("https://1admin-ap.ischool.com.tw/dsacn/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.evl.student", {
                "@": ['Type'],
                Type: 'PassportAccessToken',
                AccessToken: vars.access_token
            });
            studentConn.OnLoginError(function (err) {
                $scope.loginProgress = false;
                if (err.XMLHttpRequest.responseText.indexOf("User doesn't exist") > 0) {
                    $scope.loginError = "账号设定错误。";
                    $scope.current.mode = "view";
                }
                else {
                    $scope.current.mode = "view";
                }
                $scope.$apply($scope.reflash);
            });
            studentConn.ready(function () {
                $scope.loginProgress = false;

                studentConn.send({
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


                        $scope.$apply(function () {
                            $scope.reload = function () {
                                studentConn.send({
                                    service: 'GetSelfAssessment',
                                    autoRetry: true,
                                    result: function (resp, errorInfo, XMLHttpRequest) {
                                        $scope.reflashTick = 60;
                                        $scope.$apply(function () {
                                            var gradeString = ['一年级', '二年级', '三年级', '四年级', '五年级'];
                                            if (parseInt(resp.gradeYear, 10)) {
                                                // 2015/9/2 all gradeYear levelMax = 2
                                                // if (parseInt(resp.gradeYear, 10) <= 2)
                                                //     $scope.current.levelMax = 3;
                                                // else
                                                //     $scope.current.levelMax = 2;
                                                $scope.current.levelMax = 2;
                                                $scope.current.gradeYear = parseInt(resp.gradeYear, 10);
                                                $scope.current.grade = gradeString[parseInt(resp.gradeYear, 10) - 1];
                                            }
                                            $scope.current.gander = resp.gander;
                                            $scope.current.student = { name: resp.name };
                                            if (resp.timing.start && resp.timing.end) {
                                                $scope.timing = {
                                                    start: parseInt(resp.timing.start, 10),
                                                    end: parseInt(resp.timing.end, 10),
                                                    msg: ($scope.timing && $scope.timing.msg) ? $scope.timing.msg : ""
                                                };
                                            }
                                            //if (!$scope.countTimer)
                                            //    countTime();
                                            $scope.current.mode = 'eval';

                                            var assessmentValueCount = 0;
                                            var mateCount = 0;
                                            [].concat(resp.assessment || []).forEach(function (asm) {
                                                if (!$scope.current.assessment[asm.ref_club_id]) {
                                                    $scope.current.assessment[asm.ref_club_id] = asm.detial.Assessment;
                                                    $scope.current.mate[asm.ref_club_id] = [];
                                                    [].concat(asm.mate || []).forEach(function (mate) {
                                                        var item={
                                                            id: mate.id,
                                                            name: mate.name,
                                                            参与度:"",
                                                            合作力:"",
                                                            实效性:""
                                                        };
                                                        if (mate.detial.Assessment) {
                                                            item.参与度 = "" + mate.detial.Assessment.参与度;
                                                            item.合作力 = "" + mate.detial.Assessment.合作力;
                                                            item.实效性 = "" + mate.detial.Assessment.实效性;
                                                        }
                                                        mateCount++;
                                                        if (item.参与度 && item.合作力 && item.实效性)
                                                            assessmentValueCount++;
                                                        $scope.current.mate[asm.ref_club_id].push(item);

                                                    });
                                                }
                                                finished = true;
                                                ['学习态度',
                                                '探究兴趣',
                                                '合作分享',
                                                '学习成果',
                                                '学习动力',
                                                '继续学习',
                                                '感想',
                                                '家长评价'].forEach(function (title) {
                                                    if (asm.detial.Assessment[title])
                                                        assessmentValueCount++;
                                                });
                                            });

                                            $scope.current.selected = [null, null, null];
                                            $scope.courseList.forEach(function (course) {
                                                if (course.id == resp.selected.phase1) {
                                                    $scope.current.selected[0] = course;
                                                    $scope.current.assessment[course.id] = $scope.current.assessment[course.id] || {};
                                                }
                                                if (course.id == resp.selected.phase2) {
                                                    $scope.current.selected[1] = course;
                                                    $scope.current.assessment[course.id] = $scope.current.assessment[course.id] || {};
                                                }
                                                if (course.id == resp.selected.phase3) {
                                                    $scope.current.selected[2] = course;
                                                    $scope.current.assessment[course.id] = $scope.current.assessment[course.id] || {};
                                                }
                                            });

                                            if ($scope.current.selected[0] && $scope.current.selected[0].fullPhase == 'true') {
                                                $scope.current.progress = 100 * assessmentValueCount / (8 + mateCount);
                                            }
                                            else {
                                                if ($scope.current.levelMax == 0) {
                                                    $scope.current.progress = 0;
                                                }
                                                else
                                                    $scope.current.progress = 100 * assessmentValueCount / (8 * $scope.current.levelMax + mateCount);

                                            }
                                            $scope.reflash();
                                        });
                                    }
                                });
                            }
                            $scope.reload();
                        });
                    }
                });

                $scope.submitAssessment = function () {
                    if ($scope.current.shown.id && $scope.current.assessment[$scope.current.shown.id]) {
                        //console.warn(JSON.stringify($scope.current.assessment[$scope.current.shown.id]));
                        studentConn.send({
                            service: 'SaveSelfAssessment',
                            body: {
                                ClubID: $scope.current.shown.id,
                                Detial: $scope.current.assessment[$scope.current.shown.id],
                                Mate: angular.copy($scope.current.mate[$scope.current.shown.id])
                            },
                            autoRetry: true,
                            result: function (resp, errorInfo, XMLHttpRequest) {
                                delete $scope.current.assessment[$scope.current.shown.id];
                                //$scope.reflashTick = 0;
                                $scope.reload();
                            }
                        });
                    }
                }
            });
        }
        else {
            $scope.current.mode = "view";
        }
    }
    else {
        $scope.current.mode = "view";
    }

    $scope.parseInt = window.parseInt;

    $scope.checkValue = function (list) {
        var finished = true;
        [].concat(list || []).forEach(function (title) {
            if (!$scope.current.assessment[$scope.current.shown.id] || !$scope.current.assessment[$scope.current.shown.id][title])
                finished = false;
        });
        return !finished;
    }

    $scope.checkMate = function (ref_club_id) {
        for (var i in $scope.current.mate[ref_club_id]) {
            var item = $scope.current.mate[ref_club_id][i];
            if (!item.参与度 || !item.合作力 || !item.实效性)
                return true;
        }
        return false;
    }

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
                if ((($scope.current.mode == "eval" || $scope.current.mode == "end") && (course.limit[$scope.current.grade].all || course.limit[$scope.current.grade].all == 0)) ||
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
                    if (first == null && $scope.hasSelectedCourse(course))
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
    $scope.hasSelectedCourse = function (courselist) {
        var result = false;
        [].concat(courselist || []).forEach(function (course) {
            if ($scope.current.selected[0] == course
                || $scope.current.selected[1] == course
                || $scope.current.selected[2] == course)
                result = true;
        });
        return result;
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
    $scope.rowClass = function (course) {
        for (var i = 0; i < $scope.current.levelMax; i++) {
            if ($scope.current.selected[i] == course)
                return "success";
        }
        if ($scope.current.shown == course)
            return "warning";
        return "";
    };
    $scope.login = function () {
        window.location.assign("https://qschool.benq.com.cn/logout.php?next=" + encodeURIComponent("oauth/authorize.php?application=" + application + (dn ? ("&dn=" + dn) : "") + "&client_id=" + clientID + "&response_type=token&redirect_uri=" + redirect_uri + "&scope=" + application + ":MOD_Club.Zizhu.evl.student"));
    }
    $scope.logout = function () {
        window.location.href = location.href.substr(0, location.href.lastIndexOf('#'));
    }
    var countTime = function () {
        if (!document.hasFocus()) {
            $scope.countTimer = $timeout(countTime, 1000);
            return;
        }
        $scope.reflashTick--;
        function getTimeString(timespan) {
            function pad(num, size) {
                var s = "000000000" + num;
                return s.substr(s.length - size);
            }
            if (timespan < 60) {//一分內
                return "00:00:" + pad(timespan, 2);
            }
            else if (timespan < 3600) {//一小時內
                return "00:" + pad(parseInt(timespan / 60, 10), 2) + ":" + pad(timespan % 60, 2);
            }
            else if (timespan < 86400) {//一天內
                return pad(parseInt(timespan / 3600), 2) + ":" + pad(parseInt((timespan % 3600) / 60, 10), 2) + ":" + pad(timespan % 60, 2);
            }
            else {
                return parseInt(timespan / 86400, 10) + "天"
            }
        }
        if ($scope.timing) {            
            var now = new Date().getTime();
            if (now >= $scope.timing.end) {
                $scope.timing.msg = "已经结束";
                $scope.current.mode = 'end'
                $scope.reflash();
                delete $scope.countTimer;
            }
            else {
                if (now >= $scope.timing.start) {
                    $scope.timing.msg = "将于" + getTimeString(parseInt(($scope.timing.end - now) / 1000, 10)) + "后结束";

                    if ($scope.current.student) {
                        if ($scope.current.mode != 'eval') {
                            $scope.current.mode = 'eval';
                            $scope.reflash();
                        }
                    }
                    else {
                        if ($scope.current.mode != 'view') {
                            $scope.current.mode = 'view';
                            $scope.reflash();
                        }
                    }
                }
                else {
                    $scope.timing.msg = "将于" + getTimeString(parseInt(($scope.timing.start - now) / 1000, 10)) + "后开始";
                    if ($scope.current.mode != 'view') {
                        $scope.current.mode = 'view';
                        $scope.reflash();
                    }
                }
            }
        }
        else {
            if ($scope.current.mode != 'view') {
                $scope.current.mode = 'view';
                $scope.reflash();
            }
        }
        $scope.countTimer = $timeout(countTime, 1000);

        if ($scope.reflashTick <= 0)
            $scope.reload();
    }
}
]);