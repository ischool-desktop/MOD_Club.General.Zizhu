angular.module('MyApp', [])
	.controller('MyController', ['$scope', '$timeout', function ($scope, $timeout) {
	    $scope.connection = dsutil.creatConnection("https://1admin-ap.ischool.com.tw/dsacn/zzxx.mhedu.sh.cn/MOD_Club.Zizhu.evl.student", {
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

	    $scope.hasG1 = false;
	    $scope.hasG2 = false;
	    $scope.hasG3 = false;
	    $scope.hasG4 = false;
	    $scope.hasG5 = false;

	    $scope.connection.send({
	        service: "GetAssessmentReport",
	        body: '',
	        result: function (response, error, http) {
	            $scope.$apply(function () {
	                console.log(response);
	                $scope.data = {
	                    "学生姓名": response.Name,
	                    "五育目标": {
	                        "文学与艺术": [response['五育目标']['文学与艺术']['total'], Math.abs(response['五育目标']['文学与艺术']['grade1']), Math.abs(response['五育目标']['文学与艺术']['grade2']), Math.abs(response['五育目标']['文学与艺术']['grade3']), Math.abs(response['五育目标']['文学与艺术']['grade4']), Math.abs(response['五育目标']['文学与艺术']['grade5'])],
	                        "社会与生活": [response['五育目标']['社会与生活']['total'], Math.abs(response['五育目标']['社会与生活']['grade1']), Math.abs(response['五育目标']['社会与生活']['grade2']), Math.abs(response['五育目标']['社会与生活']['grade3']), Math.abs(response['五育目标']['社会与生活']['grade4']), Math.abs(response['五育目标']['社会与生活']['grade5'])],
	                        "运动与生命": [response['五育目标']['运动与生命']['total'], Math.abs(response['五育目标']['运动与生命']['grade1']), Math.abs(response['五育目标']['运动与生命']['grade2']), Math.abs(response['五育目标']['运动与生命']['grade3']), Math.abs(response['五育目标']['运动与生命']['grade4']), Math.abs(response['五育目标']['运动与生命']['grade5'])],
	                        "科技与创新": [response['五育目标']['科技与创新']['total'], Math.abs(response['五育目标']['科技与创新']['grade1']), Math.abs(response['五育目标']['科技与创新']['grade2']), Math.abs(response['五育目标']['科技与创新']['grade3']), Math.abs(response['五育目标']['科技与创新']['grade4']), Math.abs(response['五育目标']['科技与创新']['grade5'])],
	                        "世界与未来": [response['五育目标']['世界与未来']['total'], Math.abs(response['五育目标']['世界与未来']['grade1']), Math.abs(response['五育目标']['世界与未来']['grade2']), Math.abs(response['五育目标']['世界与未来']['grade3']), Math.abs(response['五育目标']['世界与未来']['grade4']), Math.abs(response['五育目标']['世界与未来']['grade5'])]
	                    },
	                    "成绩明细": []
	                }

	                var items = [].concat(response['成绩明细'] || []);
	                items.forEach(function (item) {
	                    var temp = {
	                        id: [item['ClubID'], item['課程']].join('-'),
	                        chart_categories: [],
	                        chart_data: [],
	                        "年級": Math.abs(item['年級']),
	                        "五育": item['五育'],
	                        "課程": item['課程'],
	                        "总体评价": {
	                            "分數": Math.abs(item['总体评价']['分數']),
	                            "文字": item['总体评价']['文字'],
	                            "級距": {
	                                categories: ['需努力', '合格', '良好', '优秀'],
	                                data: [
										{ name: '需努力', y: Math.abs(item['总体评价']['級距']['需努力']), sliced: item['总体评价']['文字'] == '需努力', selected: item['总体评价']['文字'] == '需努力' },
										{ name: '合格', y: Math.abs(item['总体评价']['級距']['合格']), sliced: item['总体评价']['文字'] == '合格', selected: item['总体评价']['文字'] == '合格' },
										{ name: '良好', y: Math.abs(item['总体评价']['級距']['良好']), sliced: item['总体评价']['文字'] == '良好', selected: item['总体评价']['文字'] == '良好' },
										{ name: '优秀', y: Math.abs(item['总体评价']['級距']['优秀']), sliced: item['总体评价']['文字'] == '优秀', selected: item['总体评价']['文字'] == '优秀' }
	                                ]
	                            }
	                        },
	                        "成绩": {}
	                    };
	                    if (temp.年級 == 1)
	                        $scope.hasG1 = true;
	                    if (temp.年級 == 2)
	                        $scope.hasG2 = true;
	                    if (temp.年級 == 3)
	                        $scope.hasG3 = true;
	                    if (temp.年級 == 4)
	                        $scope.hasG4 = true;
	                    if (temp.年級 == 5)
	                        $scope.hasG5 = true;

	                    temp['成绩']['学习力'] = [];
	                    for (var k in item['AssessmentDetial']['TeacherAssessment']) {
	                        if (k === '教师的话') {
	                            temp['成绩'][k] = item['AssessmentDetial']['TeacherAssessment'][k];
	                        } else {
	                            var t = k.split('.');
	                            temp['成绩'][t[1]] = {
	                                "文字": item['AssessmentDetial']['TeacherAssessment'][k],
	                                "分數": [3, 2, 1][
										['A', 'B', 'C'].indexOf(item['AssessmentDetial']['TeacherAssessment'][k].substr(0, 1))
	                                ]
	                            };
	                            if (t[0] === '学习力') {
	                                temp['成绩']['学习力'].push({
	                                    key: t[1],
	                                    value: item['AssessmentDetial']['TeacherAssessment'][k]
	                                });
	                            }

	                            temp.chart_categories.push(t[1]);
	                            temp.chart_data.push(temp['成绩'][t[1]]['分數'] || 0);
	                        }
	                    }

	                    for (var k in item['AssessmentDetial']['SelfAssessment']) {
	                        temp['成绩'][k] = item['AssessmentDetial']['SelfAssessment'][k];
	                    }

	                    for (var k in item['AssessmentDetial']['MateAssessment']) {
	                        temp['成绩'][k] = {
	                            "文字": item['AssessmentDetial']['MateAssessment'][k]['文字'],
	                            "分數": Math.abs(item['AssessmentDetial']['MateAssessment'][k]['分數'])
	                        }
	                        temp.chart_categories.push(k);
	                        temp.chart_data.push(temp['成绩'][k]['分數'] || 0);
	                    }

	                    $scope.data['成绩明细'].push(temp);
	                });
	            });
	            $scope.$apply(function () {
	                if ($scope.hasG5)
	                    $scope.toggleGrade(5);
	                else if ($scope.hasG4)
	                    $scope.toggleGrade(4);
	                else if ($scope.hasG3)
	                    $scope.toggleGrade(3);
	                else if ($scope.hasG2)
	                    $scope.toggleGrade(2);
	                else if ($scope.hasG1)
	                    $scope.toggleGrade(1);
	            });
	        }
	    });

	    $scope.toggleGrade = function (grade) {
	        if (!$scope["hasG" + grade])
	            return;

	        $scope.grade = grade;

	        $scope.data['成绩明细'].forEach(function (item) {
	            if (item['年級'] === grade) {
	                try {
	                    $(['#', item.id, '_1'].join('')).highcharts({
	                        chart: {
	                            polar: true,
	                            type: 'line'
	                        },
	                        title: {
	                            text: ['总体评价：', item['总体评价']['分數'], '(', item['总体评价']['文字'], ')'].join('')
	                        },
	                        pane: {
	                            size: '65%'
	                        },
	                        xAxis: {
	                            categories: item.chart_categories,
	                            tickmarkPlacement: 'on',
	                            lineWidth: 0
	                        },
	                        yAxis: {
	                            gridLineInterpolation: 'polygon',
	                            lineWidth: 0,
	                            min: 0,
	                            max: 3,
	                            tickInterval: 1
	                        },
	                        tooltip: {
	                            formatter: function () {
	                                return [this.x, ' : ', this.y].join('');
	                            }
	                        },
	                        legend: {
	                            enabled: false
	                        },
	                        credits: {
	                            enabled: false
	                        },
	                        series: [{
	                            name: '',
	                            data: item.chart_data,
	                            pointPlacement: 'on'
	                        }]
	                    });
	                }
	                catch (ex) { }
	                try {
	                    $(['#', item.id, '_2'].join('')).highcharts({
	                        chart: {
	                            plotBackgroundColor: null,
	                            plotBorderWidth: null,
	                            plotShadow: false,
	                            type: 'pie'
	                        },
	                        title: {
	                            text: '评价分布'
	                        },
	                        //tooltip: {
	                        //    formatter: function() {
	                        //        return [this.series.name, ' : ',  this.y].join('');
	                        //    }
	                        //},
	                        plotOptions: {
	                            pie: {
	                                allowPointSelect: false,
	                                cursor: 'pointer',
	                                dataLabels: {
	                                    enabled: true,
	                                    format: '{point.name}: {point.y} 人',
	                                    style: {
	                                        fontFamily: "SimSun",
	                                        fontSize: '11px',
	                                        fontWeight: 'normal',
	                                        color: '#606060'
	                                    }
	                                }
	                            }
	                        },
	                        legend: {
	                            enabled: false
	                        },
	                        credits: {
	                            enabled: false
	                        },
	                        series: [{
	                            name: '人数',
	                            colorByPoint: true,
	                            data: item['总体评价']['級距']['data'],
	                            pointPlacement: 'on'
	                        }]
	                    });
	                }
	                catch (ex) { }
	            }
	        });
	    }
	}]);
