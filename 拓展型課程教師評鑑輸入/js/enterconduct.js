angular.module('enterconduct', [])

.controller('MainCtrl', ['$scope', '$timeout',
    function ($scope, $timeout) {
        //$scope.connection = gadget.getContract("ibsh.exam.teacher");
        $scope.connection = dsutil.creatConnection("https://1admin-ap.ischool.com.tw/dsacn/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.evl.teacher", {
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


        $scope.getNow = function () {
            $scope.connection.send({
                service: "GetConfig",
                body: '',
                result: function (response, error, http) {
                    if (error !== null) {
                        // $scope.set_error_message('#mainMsg', 'GetConfig', error);
                    } else {
                        // console.log(response);
                        $scope.$apply(function () {
                            if (response !== null) {
                                $scope.config = response;
                                $scope.current = {
                                    SchoolYear: response.SchoolYear,
                                    Semester: response.Semester,
                                    Period: '3'//,
                                    //Code: [{
                                    //    Key: 'M',
                                    //    Value: '3 = Meets expectations'
                                    //}, {
                                    //    Key: 'S',
                                    //    Value: '2 = Meets needs with Support'
                                    //}, {
                                    //    Key: 'N',
                                    //    Value: '1 = Not yet within expectations'
                                    //}, {
                                    //    Key: 'N/A',
                                    //    Value: '0 = Not available'
                                    //}]
                                    //Code: [{
                                    //    Key: '3',
                                    //    Value: 'M',
                                    //    Hint:'Meets expectations'
                                    //}, {
                                    //    Key: '2',
                                    //    Value: 'S',
                                    //    Hint: 'Meets needs with Support'
                                    //}, {
                                    //    Key: '1',
                                    //    Value: 'N',
                                    //    Hint: 'Not yet within expectations'
                                    //}, {
                                    //    Key: '0',
                                    //    Value: 'N/A',
                                    //    Hint: 'Not available'
                                    //}]
                                }
                            }
                            $scope.getCourseList();
                        });
                    }
                }
            });
        };

        $scope.teacherType = '';

        $scope.getCourseList = function (type) {
            $scope.connection.send({
                service: "GetClubList",
                body: '',
                result: function (response, error, http) {
                    if (error !== null) {
                        // $scope.set_error_message('#mainMsg', 'GetExamScore', error);
                    } else {
                        // console.log(response);
                        $scope.$apply(function () {
                            if (response !== null && response.list) {
                                $scope.current.HomeroomTeacher = true;
                                $scope.teacherType = 'homeroom';

                                var _ClassList = [].concat(response.list || []);

                                angular.forEach(_ClassList, function (item) {
                                    item.ID = item.id;
                                    item.Title = item.name;
                                    item.Type = 'homeroom';
                                    item.Show = true;
                                });

                                $scope.courseList = [].concat(_ClassList || []);

                                if ($scope.teacherType === 'homeroom') {
                                    $scope.switchTeacherType('homeroom');
                                }
                            }
                        });
                    }
                }
            });
        };

        $scope.switchTeacherType = function (type) {
            $scope.teacherType = type;

            var flag = -1;
            angular.forEach($scope.courseList, function (item, index) {
                if (item.Type === type) {
                    item.Show = true;
                    if (flag === -1) flag = index;
                } else
                    item.Show = false;
            });
            if (flag !== -1)
                $scope.selectCourse($scope.courseList[flag]);
        };

        $scope.selectCourse = function (item) {
            //if ($scope.savingSeril != 0) {
            //    //alert("saveing");
            //    return;
            //}
            $scope.currentCourse = item;

            angular.forEach($scope.courseList, function (item) {
                item.selected = false;
            });
            item.selected = true;

            delete $scope.currentStudent;
            delete $scope.currentConduct;

            // angular.forEach($scope.conductList, function(c) {
            //     angular.forEach(c.Content, function(temp) {
            //         temp.selected = false;
            //     });
            // });
            if ($scope.teacherType == 'homeroom') {
                $scope.jumpMode = "Group";
            }
            else {
                $scope.jumpMode = "All";
            }
            $scope.getStudentList();

            angular.forEach([].concat($scope.config.Config || []), function (item) {
                $scope.current.FinalOpeningC = item.FinalOpeningC;
                $scope.current.FinalBeginC = new Date(Number(item.FinalBeginC));
                $scope.current.FinalEndC = new Date(Number(item.FinalEndC));
                $scope.current.ConductList = [].concat(item.Conduct.Conducts.Conduct || []);
                angular.forEach($scope.current.ConductList, function (conduct) {
                    conduct.Item = [].concat(conduct.Item || []);
                });
            });
        };

        $scope.getStudentList = function () {
            delete $scope.studentList;
            delete $scope.currentStudent;
            delete $scope.currentConduct;


            $scope.connection.send({
                service: "GetClubMemberWithAssessment",
                body: {
                    ClubID: $scope.currentCourse.ID
                },
                result: function (response, error, http) {
                    if (error !== null) {
                        // $scope.set_error_message('#mainMsg', 'GetExamScore', error);
                    } else {
                        // console.log("student list:");
                        // console.log(response);
                        $scope.$apply(function () {
                            if (response !== null) {
                                $scope.studentList = [].concat(response.Student || []);
                                angular.forEach($scope.studentList, function (item, index) {
                                    item.order = index;
                                });

                                angular.forEach($scope.studentList, function (stu) {
                                    stu.EditConduct = {
                                        Conducts: {
                                            Conduct: []
                                        }
                                    }
                                    if (stu.Conduct && stu.Conduct.Conducts && stu.Conduct.Conducts.Conduct) {
                                        stu.Conduct.Conducts.Conduct = [].concat(stu.Conduct.Conducts.Conduct || []);
                                        angular.forEach(stu.Conduct.Conducts.Conduct, function (conduct) {
                                            conduct.Item = [].concat(conduct.Item || []);
                                        });
                                    }

                                    angular.forEach($scope.current.ConductList, function (conduct) {
                                        if ($scope.teacherType === 'homeroom') {
                                            if (!conduct.Subject || conduct.Subject === '') {
                                                var _conduct = {
                                                    Group: conduct.Group,
                                                    Item: [],
                                                    ScoringScale: '',
                                                    Code: []
                                                };
                                                var code = [{
                                                    Key: '3',
                                                    Value: '优秀',
                                                    Hint: '',
                                                    Color: 'DarkGreen'
                                                }, {
                                                    Key: '2',
                                                    Value: '良好',
                                                    Hint: '',
                                                    Color: 'MediumBlue'
                                                }, {
                                                    Key: '1',
                                                    Value: '及格',
                                                    Hint: '',
                                                    Color: 'SaddleBrown'
                                                }, {
                                                    Key: '0',
                                                    Value: '需努力',
                                                    Hint: '',
                                                    Color: 'Gray'
                                                }];
                                                _conduct.ScoringScale = "2015";
                                                _conduct.Code = code;
                                                angular.forEach(conduct.Item, function (item) {
                                                    _conduct.Item.push({
                                                        Title: item.Title || '',
                                                        Grade: '',
                                                        Group: conduct.Group,
                                                        Code: code
                                                    });
                                                });

                                                stu.EditConduct.Conducts.Conduct.push(_conduct);
                                            }
                                        }
                                    });
                                    angular.forEach(stu.EditConduct.Conducts.Conduct, function (ec) {
                                        ec.Item = [].concat(ec.Item || []);
                                        angular.forEach(ec.Item, function (ei) {
                                            var key = ec.Group + "." + ei.Title;
                                            if (stu.Assessment && stu.Assessment.Assessment && stu.Assessment.Assessment[key])
                                                ei.Grade = stu.Assessment.Assessment[key];
                                        });
                                    });
                                    if (stu.Assessment && stu.Assessment.Assessment && stu.Assessment.Assessment['教师的话'])
                                        stu.Comment = stu.Assessment.Assessment['教师的话'];
                                });

                                //console.log($scope.studentList);

                                if ($scope.studentList.length) {
                                    $scope.selectStudent($scope.studentList[0]);
                                }
                            }
                        });
                    }
                }
            });
        };

        $scope.selectStudent = function (item, skipAutoSelect) {
            //if ($scope.savingSeril != 0) {
            //    //alert("saveing");
            //    return;
            //}

            if (!item) return;

            $scope.currentStudent = item;
            $scope.currentStudent.tempStudentID = $scope.currentStudent.StudentID;

            angular.forEach($scope.studentList, function (item) {
                item.selected = false;
            });
            item.selected = true;

            if (!skipAutoSelect) {
                if ($scope.currentStudent.EditConduct.Conducts.Conduct.length > 0 && $scope.currentStudent.EditConduct.Conducts.Conduct[0].Item.length > 0) {
                    var period = 1;
                    var conduct = null;
                    if ($scope.currentConduct) {
                        if ($scope.currentConduct.type != 'Comment') {
                            period = $scope.currentConduct.Period;
                            conduct = null;
                            $scope.currentStudent.EditConduct.Conducts.Conduct.forEach(function (cond) {
                                if (cond.Group == $scope.currentConduct.Group) {
                                    cond.Item.forEach(function (item) {
                                        if (item.Title == $scope.currentConduct.Title) {
                                            conduct = item;
                                        }
                                    });
                                }
                            });
                            if (conduct == null) {
                                conduct = $scope.currentStudent.EditConduct.Conducts.Conduct[0].Item[0];
                            }
                        }
                        else {
                            conduct = $scope.currentConduct;
                            period = $scope.currentConduct.Period;
                        }
                    }
                    else {
                        if ($scope.currentCourse.GradeYear <= 4) {
                            if ($scope.current.MiddleOpeningC === 'true') {
                                period = 1;
                            } else if ($scope.current.FinalOpeningC === 'true') {
                                period = 2;
                            }
                        } else {
                            if ($scope.current.FinalOpeningC === 'true') {
                                period = 3;
                            }
                        }
                        conduct = $scope.currentStudent.EditConduct.Conducts.Conduct[0].Item[0];
                    }
                    $scope.selectConduct(conduct, period);
                }
            }


            //$('.sna').addClass('stuName');
            //if (!$scope.wait) {
            //    $timeout.cancel($scope.wait)
            //}
            //$scope.wait = $timeout(function () {
            //    $('.stuName').removeClass('stuName');
            //    delete $scope.wait;
            //}, 1500);
        };

        $scope.selectStudentConduct = function (student, conduct, period) {
            $('#modalOverview').modal('hide');
            $scope.selectConduct(conduct, period);
            $scope.selectStudent(student);
        }

        $scope.setJumpMode = function (mode) {
            $scope.jumpMode = mode;
        }

        $scope.selectNextStudent = function () {
            //if ($scope.savingSeril != 0) {
            //    //alert("saveing");
            //    return;
            //}
            var currentIndex = $scope.currentStudent.order ? $scope.currentStudent.order : 0;
            if ($scope.studentList.length - 1 == currentIndex)
                var nextIndex = 0;
            else
                var nextIndex = currentIndex + 1;
            var nextStudent = $scope.studentList[nextIndex];
            $scope.selectStudent(nextStudent);
        }

        $scope.selectPrevStudent = function () {
            //if ($scope.savingSeril != 0) {
            //    //alert("saveing");
            //    return;
            //}
            var currentIndex = $scope.currentStudent.order ? $scope.currentStudent.order : 0;
            if (currentIndex == 0)
                var nextIndex = $scope.studentList.length - 1;
            else
                var nextIndex = currentIndex - 1;
            var nextStudent = $scope.studentList[nextIndex];
            $scope.selectStudent(nextStudent);
        }

        $scope.selectStudentID = function (event) { //輸入座號跳到該座號
            //if ($scope.savingSeril != 0) {
            //    //alert("saveing");
            //    return;
            //}
            if (event.keyCode !== 13) return; // 13是enter按鈕的代碼，return是跳出

            if (!$scope.currentStudent) return;

            var nextStudent = null;
            angular.forEach($scope.studentList, function (item) {
                if (item.StudentID === $scope.currentStudent.tempStudentID) {
                    nextStudent = item;
                }
            });

            $scope.selectStudent(nextStudent);
        };

        $scope.calcProgress = function () {
            //currentConduct.Period
            var max = 0, progress = 0;
            angular.forEach($scope.currentStudent.EditConduct.Conducts.Conduct, function (conduct) {
                angular.forEach(conduct.Item, function (item) {
                    max++;
                    if ($scope.currentConduct.Period === 1 && item.MidtermGrade)
                        progress++

                    if ($scope.currentConduct.Period === 2 && item.FinalGrade)
                        progress++

                    if ($scope.currentConduct.Period === 3 && item.Grade)
                        progress++
                });
            });
            if ($scope.teacherType == 'homeroom') {
                if ($scope.currentConduct.Period === 3) {
                    max++;
                    if ($scope.currentStudent.Comment)
                        progress++;
                }
            }
            var pwidth = "0%";
            if (max > 0) pwidth = "" + Math.floor(progress * 100 / max) + "%";
            return {
                progress: "" + progress + " / " + max,
                width: pwidth
            };
        }

        $scope.selectConduct = function (item, period) {
            $scope.currentConduct = item;
            $scope.currentConduct.Period = period;

            angular.forEach($scope.currentStudent.EditConduct.Conducts.Conduct, function (c) {
                angular.forEach(c.Item, function (temp, index) {
                    temp.selected = false;
                    temp.index = index + 1;
                });
            });
            if (item.type == 'Comment') {
                if (period === 1) {
                    $scope.currentConduct.tempGrade = $scope.currentStudent.MidtermComment;
                    if ($scope.current.MiddleOpeningC === 'true') {
                        item.canInputGrade = true;
                        //item.canInputMComment = true;
                        //item.canInputFComment = false;
                    } else {
                        item.canInputGrade = false;
                        //item.canInputMComment = false;
                        //item.canInputFComment = false;
                    }
                } else if (period === 2) {
                    $scope.currentConduct.tempGrade = $scope.currentStudent.FinalComment;
                    if ($scope.current.FinalOpeningC === 'true') {
                        item.canInputGrade = true;
                        //item.canInputMComment = false;
                        //item.canInputFComment = true;
                    } else {
                        item.canInputGrade = false;
                        //item.canInputMComment = false;
                        //item.canInputFComment = false;
                    }
                } else {
                    $scope.currentConduct.tempGrade = $scope.currentStudent.Comment;
                    if ($scope.current.FinalOpeningC === 'true') {
                        item.canInputGrade = true;
                    } else {
                        item.canInputGrade = false;
                    }
                }
                $timeout(function () {
                    $('#comment-textarea').focus().select();
                }, 100);
            }
            else {
                item.selected = true;

                if (period === 1) {
                    $scope.currentConduct.tempGrade = item.MidtermGrade;
                    if ($scope.current.MiddleOpeningC === 'true') {
                        item.canInputGrade = true;
                        //item.canInputMComment = true;
                        //item.canInputFComment = false;
                    } else {
                        item.canInputGrade = false;
                        //item.canInputMComment = false;
                        //item.canInputFComment = false;
                    }
                } else if (period === 2) {
                    $scope.currentConduct.tempGrade = item.FinalGrade;
                    if ($scope.current.FinalOpeningC === 'true') {
                        item.canInputGrade = true;
                        //item.canInputMComment = false;
                        //item.canInputFComment = true;
                    } else {
                        item.canInputGrade = false;
                        //item.canInputMComment = false;
                        //item.canInputFComment = false;
                    }
                } else {
                    $scope.currentConduct.tempGrade = item.Grade;
                    if ($scope.current.FinalOpeningC === 'true') {
                        item.canInputGrade = true;
                    } else {
                        item.canInputGrade = false;
                    }
                }
                $timeout(function () {
                    $('#grade-textbox').focus().select();
                }, 100);
            }
        };

        $scope.enterGrade = function (event) {
            if (event.keyCode !== 13) return;

            if (!$scope.currentConduct) return;

            var grade = $scope.currentConduct.tempGrade.toUpperCase();

            var flag = false;
            if (grade == "") {
                //flag = true;
            } else {
                angular.forEach($scope.currentConduct.Code, function (item) {
                    if (item.Key.toUpperCase() === grade || item.Value.toUpperCase() === grade) {
                        grade = item.Value;
                        flag = true;
                        $scope.currentConduct.tempGrade = grade;
                    }
                });
            }
            if (flag) {
                if ($scope.currentConduct.Period === 1)
                    $scope.currentConduct.MidtermGrade = $scope.currentConduct.tempGrade;
                else if ($scope.currentConduct.Period === 2)
                    $scope.currentConduct.FinalGrade = $scope.currentConduct.tempGrade;
                else
                    $scope.currentConduct.Grade = $scope.currentConduct.tempGrade;
                $scope.saveGrade($scope.currentStudent, $scope.currentConduct);
                $('#grade-textbox').blur();//不把focus踢掉會造成捲回TOP
                $scope.nextItem();
            }

        };

        $scope.enterComment = function (event) {
            if (event.keyCode !== 13 || event.shiftKey) return;

            if (!$scope.currentConduct) return;

            $scope.currentConduct.tmpGrade = $scope.currentConduct.tempGrade.trim();

            if ($scope.currentConduct.Period === 1)
                $scope.currentStudent.MidtermComment = $scope.currentConduct.tempGrade;
            else if ($scope.currentConduct.Period === 2)
                $scope.currentStudent.FinalComment = $scope.currentConduct.tempGrade;
            else
                $scope.currentStudent.Comment = $scope.currentConduct.tempGrade;
            $scope.saveGrade($scope.currentStudent, $scope.currentConduct);
            $('#comment-textarea').blur();//不把focus踢掉會造成捲回TOP
            $scope.nextItem();
        };

        $scope.nextItem = function () {
            var nextConduct = null,
                period = $scope.currentConduct.Period,
                isFirstGroup = false,
                isFirstItem = false,
                isLastGroup = false,
                isLastItem = false,
                groupIndex = 0,
                itemIndex = 0,
                isComment = false;

            if ($scope.currentConduct.type == 'Comment') {
                isFirstItem = true;
                isLastItem = true;
                isLastGroup = true;
                isComment = true;
                if ($scope.currentStudent.EditConduct.Conducts.Conduct.length == 0)
                    isFirstGroup = true;
            }
            else {
                angular.forEach($scope.currentStudent.EditConduct.Conducts.Conduct, function (conduct, i) {
                    if ($scope.currentConduct.Group === conduct.Group) {
                        groupIndex = i;
                        if (i == 0)
                            isFirstGroup = true;
                        if (i == $scope.currentStudent.EditConduct.Conducts.Conduct.length - 1
                            && $scope.teacherType != 'homeroom')
                            isLastGroup = true;

                        angular.forEach(conduct.Item, function (item, j) {
                            if ($scope.currentConduct.Title === item.Title) {
                                itemIndex = j;
                                if (j == 0)
                                    isFirstItem = true;
                                if (j == conduct.Item.length - 1)
                                    isLastItem = true;
                            }
                        });
                    }
                });
            }

            if (isLastItem) {
                if ($scope.jumpMode == 'All') {
                    if (isLastGroup) {
                        if ($scope.currentStudent.order != $scope.studentList.length - 1) {
                            $scope.selectStudent($scope.studentList[$scope.currentStudent.order + 1], true);
                        }
                        else {
                            $scope.selectStudent($scope.studentList[0], true);
                        }
                        nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[0].Item[0];
                    }
                    else {
                        //明明是最後一個卻不是LastGroup表示還有Comment
                        if ($scope.currentStudent.EditConduct.Conducts.Conduct.length - 1 != groupIndex)
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex + 1].Item[0];
                        else {
                            nextConduct = { type: 'Comment' };
                        }
                    }
                }
                if ($scope.jumpMode == 'Group') {
                    if ($scope.currentStudent.order != $scope.studentList.length - 1) {
                        $scope.selectStudent($scope.studentList[$scope.currentStudent.order + 1], true);
                        if (!isComment)
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex].Item[0];
                        else
                            nextConduct = { type: 'Comment' };

                    }
                    else {
                        $scope.selectStudent($scope.studentList[0], true);
                        if (isLastGroup) {
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[0].Item[0];
                        }
                        else {
                            //明明是最後一個卻不是LastGroup表示還有Comment
                            if ($scope.currentStudent.EditConduct.Conducts.Conduct.length - 1 != groupIndex)
                                nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex + 1].Item[0];
                            else {
                                nextConduct = { type: 'Comment' };
                            }
                        }
                    }
                }
                try {
                    //自動捲動

                    //function scrollTo(to, duration) {
                    //    if (document.body.scrollTop == to) return;
                    //    if ($scope.scrollInterval) {
                    //        clearInterval($scope.scrollInterval);
                    //        delete $scope.scrollInterval;
                    //    }
                    //    var diff = to - document.body.scrollTop;
                    //    var scrollStep = Math.PI / (duration / 10);
                    //    var count = 0, currPos;
                    //    var start = document.body.scrollTop;
                    //    $scope.scrollInterval = setInterval(function () {
                    //        if (document.body.scrollTop - to > 10 || document.body.scrollTop - to < -10) {
                    //            count = count + 1;
                    //            if (count * scrollStep > Math.PI) {
                    //                clearInterval($scope.scrollInterval);
                    //                delete $scope.scrollInterval;
                    //            }
                    //            else {
                    //                currPos = start + diff * (0.5 - 0.5 * Math.cos(count * scrollStep));
                    //                try {
                    //                    window.scrollTo(0, currPos);
                    //                    //document.body.scrollTop = currPos;
                    //                }
                    //                catch (exc) {
                    //                    clearInterval($scope.scrollInterval);
                    //                    delete $scope.scrollInterval;
                    //                }
                    //            }
                    //        }
                    //        else {
                    //            clearInterval($scope.scrollInterval);
                    //            delete $scope.scrollInterval;
                    //        }
                    //    }, 10);
                    //}
                    //function getPosition(element) {
                    //    var xPosition = 0;
                    //    var yPosition = 0;

                    //    while (element) {
                    //        xPosition += (element.offsetLeft - element.scrollLeft + element.clientLeft);
                    //        yPosition += (element.offsetTop - element.scrollTop + element.clientTop);
                    //        element = element.offsetParent;
                    //    }
                    //    return { x: xPosition, y: yPosition };
                    //}
                    if (nextConduct.type != 'Comment') {
                        //scrollTo(getPosition($('#scrollAnchor' + $scope.fixGroupName(nextConduct.Group))[0]).y, 500);
                        $('#scrollAnchor' + $scope.fixGroupName(nextConduct.Group))[0].scrollIntoView({ block: "end", behavior: "smooth" });
                    }
                    else {
                        //scrollTo(getPosition($('#scrollAnchorComment')[0]).y, 500);
                        $('#scrollAnchorComment')[0].scrollIntoView({ block: "end", behavior: "smooth" });
                    }
                }
                catch (exc) {

                }
            }
            else {
                nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex].Item[itemIndex + 1];
            }

            $scope.selectConduct(nextConduct, period);
        }

        $scope.fixGroupName = function (name) {
            return escape(name).replace(/%/g, '_');
            return name.replace(/ /g, '_');
        }

        $scope.prevItem = function () {
            var nextConduct = null,
                period = $scope.currentConduct.Period,
                isFirstGroup = false,
                isFirstItem = false,
                isLastGroup = false,
                isLastItem = false,
                groupIndex = 0,
                itemIndex = 0,
                isComment = false;

            if ($scope.currentConduct.type == 'Comment') {
                isFirstItem = true;
                isLastItem = true;
                isLastGroup = true;
                isComment = true;
                groupIndex = $scope.currentStudent.EditConduct.Conducts.Conduct.length;
                if ($scope.currentStudent.EditConduct.Conducts.Conduct.length == 0)
                    isFirstGroup = true;
            }
            else {
                angular.forEach($scope.currentStudent.EditConduct.Conducts.Conduct, function (conduct, i) {
                    if ($scope.currentConduct.Group === conduct.Group) {
                        groupIndex = i;
                        if (i == 0)
                            isFirstGroup = true;
                        if (i == $scope.currentStudent.EditConduct.Conducts.Conduct.length - 1
                            && $scope.teacherType != 'homeroom')
                            isLastGroup = true;

                        angular.forEach(conduct.Item, function (item, j) {
                            if ($scope.currentConduct.Title === item.Title) {
                                itemIndex = j;
                                if (j == 0)
                                    isFirstItem = true;
                                if (j == conduct.Item.length - 1)
                                    isLastItem = true;
                            }
                        });
                    }
                });
            }

            if (isFirstItem) {
                if ($scope.jumpMode == 'All') {
                    if (isFirstGroup) {
                        if ($scope.currentStudent.order != 0) {
                            $scope.selectStudent($scope.studentList[$scope.currentStudent.order - 1], true);
                        }
                        else {
                            $scope.selectStudent($scope.studentList[$scope.studentList.length - 1], true);
                        }
                        if ($scope.teacherType != 'homeroom') {
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[$scope.currentStudent.EditConduct.Conducts.Conduct.length - 1].Item[$scope.currentStudent.EditConduct.Conducts.Conduct[$scope.currentStudent.EditConduct.Conducts.Conduct.length - 1].Item.length - 1];
                        }
                        else {
                            nextConduct = { type: 'Comment' };
                        }
                    }
                    else {
                        nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex - 1].Item[$scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex - 1].Item.length - 1];
                    }
                }
                if ($scope.jumpMode == 'Group') {
                    if ($scope.currentStudent.order != 0) {
                        $scope.selectStudent($scope.studentList[$scope.currentStudent.order - 1], true);
                        if (!isComment)
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex].Item[$scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex].Item.length - 1];
                        else
                            nextConduct = { type: 'Comment' };
                    }
                    else {
                        $scope.selectStudent($scope.studentList[$scope.studentList.length - 1], true);
                        if (isFirstGroup) {
                            if ($scope.teacherType != 'homeroom') {
                                nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[$scope.currentStudent.EditConduct.Conducts.Conduct.length - 1].Item[$scope.currentStudent.EditConduct.Conducts.Conduct[$scope.currentStudent.EditConduct.Conducts.Conduct.length - 1].Item.length - 1];
                            }
                            else {
                                nextConduct = { type: 'Comment' };
                            }
                        }
                        else {
                            nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex - 1].Item[$scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex - 1].Item.length - 1];
                        }
                    }
                }

                if (nextConduct.type != 'Comment') {
                    //scrollTo(getPosition($('#scrollAnchor' + $scope.fixGroupName(nextConduct.Group))[0]).y, 500);
                    $('#scrollAnchor' + $scope.fixGroupName(nextConduct.Group))[0].scrollIntoView({ block: "end", behavior: "smooth" });
                }
                else {
                    //scrollTo(getPosition($('#scrollAnchorComment')[0]).y, 500);
                    $('#scrollAnchorComment')[0].scrollIntoView({ block: "end", behavior: "smooth" });
                }
            }
            else {
                nextConduct = $scope.currentStudent.EditConduct.Conducts.Conduct[groupIndex].Item[itemIndex - 1];
            }

            $scope.selectConduct(nextConduct, period);
        }

        $scope.overview = function (conductGroup, period) {
            delete $scope.overrideVal;
            if (!!!$scope.modal) {
                $scope.modal = {};
                $scope.modal.conductGroup = conductGroup;
                $scope.modal.period = period;
                $('#modalOverview').modal('show').on('shown.bs.modal', function (e) {
                    //$('#modal input:visible:first').focus().select();
                });
            }
            else {
                $scope.modal.conductGroup = conductGroup;
                $scope.modal.period = period;
                $('#modalOverview').modal('show');
            }
        }

        $scope.getOverrideVal = function () {
            return $scope.overrideVal || '';
        }

        $scope.setOverrideVal = function (val) {
            $scope.overrideVal = val;
        }

        $scope.setVal = function (student, conduct, period, val) {
            if ($scope.savingSeril)
                return;
            if (!val) return;
            //$scope.selectConduct(conduct, period);
            //$scope.selectStudent(student);
            //$scope.clickGrade(val);
            if (period == 1 && $scope.current.MiddleOpeningC !== 'true') return;
            if (period == 2 && $scope.current.FinalOpeningC !== 'true') return;
            if (period == 3 && $scope.current.FinalOpeningC !== 'true') return;

            var match = false;
            student.EditConduct.Conducts.Conduct.forEach(function (cond) {
                if (!match && cond.Group == conduct.Group) {
                    cond.Item.forEach(function (item) {
                        if (!match && item.Title == conduct.Title) {
                            if (period == 1) {
                                item.MidtermGrade = val;
                                if ($scope.currentConduct == item && $scope.currentConduct.Period == period)
                                    $scope.currentConduct.tempGrade = item.MidtermGrade;
                            }
                            if (period == 2) {
                                item.FinalGrade = val;
                                if ($scope.currentConduct == item && $scope.currentConduct.Period == period)
                                    $scope.currentConduct.tempGrade = item.FinalGrade;
                            }
                            if (period == 3) {
                                item.Grade = val;
                                if ($scope.currentConduct == item && $scope.currentConduct.Period == period)
                                    $scope.currentConduct.tempGrade = item.Grade;
                            }
                            match = true;
                            item.Period = period;
                            $scope.saveGrade(student, item, function (resp, err) {
                                if (err) {
                                    alert('Save Value Failed.Please Try Again.' + error);
                                    $scope.selectConduct(conduct, period);
                                    $scope.selectStudent(student);
                                }
                            });
                        }
                    });
                }
            });

        }

        $scope.configDefaultValue = function (period) {
            $scope.defaultPeriod = period;
            $('#modalConfigDefaultValue').modal('show');
        }

        $scope.getVal = function (student, group, title, period) {
            var result;
            angular.forEach(student.EditConduct.Conducts.Conduct, function (cgroup) {
                if (cgroup.Group == group) {
                    angular.forEach(cgroup.Item, function (citem) {
                        if (citem.Title == title) {
                            if (period == 1)
                                result = citem.MidtermGrade;
                            if (period == 2)
                                result = citem.FinalGrade;
                            if (period == 3)
                                result = citem.Grade;
                        }
                    });
                }
            });
            return result;
        }

        $scope.getColor = function (student, group, title, period) {
            var result;
            if ($scope.savingSeril)
                return { color: '#a0a0a0' };
            angular.forEach(student.EditConduct.Conducts.Conduct, function (cgroup) {
                if (cgroup.Group == group) {
                    angular.forEach(cgroup.Item, function (citem) {
                        if (citem.Title == title) {
                            if (period == 1)
                                result = citem.MidtermGrade;
                            if (period == 2)
                                result = citem.FinalGrade;
                            if (period == 3)
                                result = citem.Grade;

                            if (result) {
                                angular.forEach(citem.Code, function (code) {
                                    if (code.Value == result) {
                                        result = { color: code.Color };
                                    }
                                })
                            }
                        }
                    });
                }
            });
            return result;
        }

        $scope.setDefault = function (gradeYear, subject, group, title, value) {
            if (!$scope.default)
                $scope.default = {};
            if (!$scope.default["" + gradeYear + " " + subject])
                $scope.default["" + gradeYear + " " + subject] = {};
            $scope.default["" + gradeYear + " " + subject]["" + group + " " + title] = value;
        }

        $scope.setGroupDefault = function (gradeYear, subject, conductGroup, value) {
            if (!$scope.default)
                $scope.default = {};
            if (!$scope.default["" + gradeYear + " " + subject])
                $scope.default["" + gradeYear + " " + subject] = {};
            conductGroup.Item.forEach(function (i) {
                $scope.default["" + gradeYear + " " + subject]["" + conductGroup.Group + " " + i.Title] = value;
            })

        }

        $scope.getDefault = function (gradeYear, subject, group, title) {
            if ($scope.default && $scope.default["" + gradeYear + " " + subject] && $scope.default["" + gradeYear + " " + subject]["" + group + " " + title])
                return $scope.default["" + gradeYear + " " + subject]["" + group + " " + title];
            else
                return '';
        }

        $scope.useDefaultValueForAll = function () {
            $scope.useDefaultValueForAllProgress = "0%";
            $scope.showDefaultValueForAllProgress = 'progress-bar-success';
            var index = 0;
            function saveNext(response, error, http) {
                if (error) {
                    alert('Setting Default Value Failed.Please Try Again.' + error);
                    $scope.showDefaultValueForAllProgress = 'progress-bar-danger';
                }
                else if (index < $scope.studentList.length) {
                    var student = $scope.studentList[index++];
                    $scope.useDefaultValueForAllProgress = Math.floor(index * 100 / $scope.studentList.length) + "%";
                    angular.forEach(student.EditConduct.Conducts.Conduct, function (conduct) {
                        angular.forEach(conduct.Item, function (item) {
                            var defVal = $scope.getDefault($scope.currentCourse.GradeYear,
                                        $scope.currentCourse.SubjectChineseName,
                                        conduct.Group,
                                        item.Title);
                            if ($scope.defaultPeriod === 1 && $scope.current.MiddleOpeningC === 'true') {
                                if (!item.MidtermGrade && defVal != 'erase') {
                                    item.MidtermGrade = defVal;
                                }
                                else {
                                    if (defVal == 'erase')
                                        item.MidtermGrade = '';
                                }
                            }

                            if ($scope.defaultPeriod === 2 && $scope.current.FinalOpeningC === 'true') {
                                if (!item.FinalGrade && defVal != 'erase') {
                                    item.FinalGrade = defVal;
                                }
                                else {
                                    if (defVal == 'erase')
                                        item.FinalGrade = '';
                                }
                            }

                            if ($scope.defaultPeriod === 3 && $scope.current.FinalOpeningC === 'true') {
                                if (!item.Grade && defVal != 'erase') {
                                    item.Grade = defVal;
                                }
                                else {
                                    if (defVal == 'erase')
                                        item.Grade = '';
                                }
                            }
                        });
                    });

                    $scope.saveGrade(student, { Period: $scope.defaultPeriod }, saveNext);
                    //setTimeout(function () { $scope.saveGrade(student, $scope.currentConduct, saveNext); }, 1);
                }
                else {
                    $timeout(function () {
                        $scope.showDefaultValueForAllProgress = '';
                    }, 2000);

                }
            }
            saveNext();
        }

        $scope.submit = function () {
            if ($scope.currentConduct.type != 'Comment') {
                var grade = $scope.currentConduct.tempGrade.toUpperCase();

                var flag = false;

                angular.forEach($scope.currentConduct.Code, function (item) {
                    if (item.Key.toUpperCase() === grade || item.Value.toUpperCase() === grade) {
                        grade = item.Value;
                        flag = true;
                        $scope.currentConduct.tempGrade = grade;
                    }
                });

                if (flag) {
                    if ($scope.currentConduct.Period === 1)
                        $scope.currentConduct.MidtermGrade = $scope.currentConduct.tempGrade;
                    else if ($scope.currentConduct.Period === 2)
                        $scope.currentConduct.FinalGrade = $scope.currentConduct.tempGrade;
                    else
                        $scope.currentConduct.Grade = $scope.currentConduct.tempGrade;
                }
                else {
                    if ($scope.currentConduct.Period === 1)
                        $scope.currentConduct.tempGrade = $scope.currentConduct.MidtermGrade;
                    else if ($scope.currentConduct.Period === 2)
                        $scope.currentConduct.tempGrade = $scope.currentConduct.FinalGrade;
                    else
                        $scope.currentConduct.tempGrade = $scope.currentConduct.Grade;
                }
            }
            else {
                $scope.currentConduct.tmpGrade = $scope.currentConduct.tempGrade.trim();

                if ($scope.currentConduct.Period === 1)
                    $scope.currentStudent.MidtermComment = $scope.currentConduct.tempGrade;
                else if ($scope.currentConduct.Period === 2)
                    $scope.currentStudent.FinalComment = $scope.currentConduct.tempGrade;
                else
                    $scope.currentStudent.Comment = $scope.currentConduct.tempGrade;
            }

            $scope.saveGrade($scope.currentStudent, $scope.currentConduct);
        }

        $scope.clickGrade = function (val) {
            $scope.currentConduct.tempGrade = val;
            $scope.enterGrade({ keyCode: 13 });
        }

        $scope.toUpperCase = function (val) {
            return ("" + val).toUpperCase();
        }

        $scope.SetDefaultValue = function () {

            var grade = $scope.currentConduct.tempGrade.toUpperCase();

            var flag = false;
            angular.forEach($scope.currentConduct.Code, function (item) {
                if (item.Key.toUpperCase() === grade || item.Value.toUpperCase() === grade) {
                    grade = item.Value;
                    flag = true;
                    $scope.currentConduct.tempGrade = grade;
                }
            });
            if (!flag)
                return;

            angular.forEach($scope.currentStudent.EditConduct.Conducts.Conduct, function (conduct) {
                angular.forEach(conduct.Item, function (item) {

                    angular.forEach(item.Code, function (c) {
                        if (c.Value === grade) {
                            if ($scope.currentConduct.Period === 1 && $scope.current.MiddleOpeningC === 'true') {
                                item.MidtermGrade = grade;
                                item.tempGrade = grade;
                            }

                            if ($scope.currentConduct.Period === 2 && $scope.current.FinalOpeningC === 'true') {
                                item.FinalGrade = grade;
                                item.tempGrade = grade;
                            }

                            if ($scope.currentConduct.Period === 3 && $scope.current.FinalOpeningC === 'true') {
                                item.Grade = grade;
                                item.tempGrade = grade;
                            }
                        }
                    });
                });
            });
            $scope.saveGrade($scope.currentStudent, $scope.currentConduct);
        };

        $scope.savingSeril = 0;
        $scope.saveGrade = function (currentStudent, currentConduct, callBack) {
            var savingSeril = new Date().getTime();
            var currentPeriod = currentConduct.Period;

            var Assessment = {};
            angular.forEach(currentStudent.EditConduct.Conducts.Conduct, function (ec) {
                ec.Item = [].concat(ec.Item || []);
                angular.forEach(ec.Item, function (ei) {
                    var key = ec.Group + "." + ei.Title;
                    Assessment[key] = ei.Grade;
                });
            });

            Assessment['教师的话'] = currentStudent.Comment || '';

            var _body = {
                StudentID: currentStudent.ID,
                ClubID:$scope.currentCourse.ID,
                Assessment: Assessment
            };

            if ($scope.savingSeril == 0) {
                $scope.savingSeril = savingSeril;
                $scope.connection.send({
                    service: "SaveAssessment",
                    body: _body,
                    result: function (response, error, http) {
                        if (error !== null) {
                            alert("Error occurred while saving data." + (error.dsaError.status || '') + (error.dsaError.message || ''));
                            $scope.$apply(function () {
                                $scope.savingSeril = 0;
                                if (callBack) {
                                    callBack(response, error, http);
                                }
                                else {
                                    $scope.selectStudent(currentStudent);
                                    $scope.selectConduct(currentConduct, currentPeriod);
                                }
                            });
                        } else {
                            //console.log(response);
                            $scope.$apply(function () {
                                if ($scope.savingSeril == savingSeril) {
                                    $scope.savingSeril = 0;
                                    if (callBack) {
                                        callBack(response, error, http);
                                    }
                                }
                                else {
                                    $scope.savingSeril = 0;
                                    $scope.saveGrade(currentStudent, currentConduct);
                                }
                            });
                        }
                    }
                });
            }
            else {
                $scope.savingSeril = savingSeril;
            }
        };

        $scope.padLeft = function (str, length) {
            if (str.length >= length) return str
            else return $scope.padLeft("0" + str, length);
        };

        $scope.studentSort = function (x, y) {
            var xx = $scope.padLeft(x.ClassName, 20);
            xx += $scope.padLeft(x.SeatNo, 3);

            var yy = $scope.padLeft(y.ClassName, 20);
            yy += $scope.padLeft(y.SeatNo, 3);

            if (xx == yy)
                return 0;

            if (xx < yy)
                return -1;
            else
                return 1;
        };

        $scope.PrintPage = function () {

            var html = "";
            var M = false;
            var F = false;
            var ClassOrCourseName = "";

            if ($scope.currentCourse.Type === "homeroom")
                ClassOrCourseName = "Class : " + $scope.currentCourse.Title;
            else
                ClassOrCourseName = "Course : " + $scope.currentCourse.Title;

            // StudentChineseName: "好將將"
            // StudentEnglishName: "JIANG, YAO-MING"
            // StudentNickname: "goodGG"
            // StudentNumber: "814201"

            angular.forEach($scope.studentList, function (value) {
                $scope.selectStudent(value);

                var studentName = "ChineseName: " + value.StudentChineseName + "  EnglishName: " + value.StudentEnglishName + "  NickName: " + value.StudentNickname + "  StudentNo: " + value.StudentNumber;

                if ($scope.currentConduct.canInputMComment) {
                    $scope.currentConduct.canInputMComment = false;
                    M = true;
                }

                if ($scope.currentConduct.canInputFComment) {
                    $scope.currentConduct.canInputFComment = false;
                    F = true;
                }

                $scope.$apply();
                html += "<p>===============================================================================</p>"
                html += "<p>" + studentName + "</p>";
                html += $('#printlist').html();
            });

            if (M)
                $scope.currentConduct.canInputMComment = true;
            if (F)
                $scope.currentConduct.canInputFComment = true;

            $scope.$apply();

            var printPage = window.open("", "printPage", "");
            printPage.document.open();
            printPage.document.write("<HTML><head></head><BODY onload='window.print();window.close()'>");
            printPage.document.write("<PRE>");
            printPage.document.write("<p>" + ClassOrCourseName + "</p>");
            printPage.document.write(html);
            printPage.document.write("<br>");
            printPage.document.write("<br>");
            printPage.document.write("Signature:______________________");
            printPage.document.write("</PRE>");
            printPage.document.close("</BODY></HTML>");
        };

        $scope.getNow();

    }
])
.controller('affixCtrl', function ($scope) {

    $(function () {
        $("#affixPanel").affix({
            offset: {
                top: 180,
                bottom: function () {
                    return (this.bottom = $('.footer').outerHeight(true))
                }
            }
        })
    });
})
