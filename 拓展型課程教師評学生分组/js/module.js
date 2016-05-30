angular.module('MyApp', [])
	.controller('MyController', ['$scope', '$timeout', function ($scope, $timeout) {
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

	    $scope.connection.send({
	        service: "GetClubList",
	        body: '',
	        result: function (response, error, http) {
	            $scope.$apply(function () {
	                $scope.courses = [].concat(response.list || []);
	                $scope.courses.forEach(function (item) {
	                    ["文学与艺术", "社会与生活", "运动与生命", "科技与创新", "世界与未来"].forEach(function (domain) {
	                        if (item.classification.indexOf(domain) !== -1)
	                            item.ClubDomain = domain;
	                    });
	                });
	                $scope.toggleCourse($scope.courses[0]);
	            });
	        }
	    });

	    $scope.Groups = [
			{ label: '第一組', title: '第一組', count: [] },
			{ label: '第二組', title: '第二組', count: [] },
			{ label: '第三組', title: '第三組', count: [] },
			{ label: '第四組', title: '第四組', count: [] },
			{ label: '第五組', title: '第五組', count: [] },
			{ label: '第六組', title: '第六組', count: [] },
			{ label: '第七組', title: '第七組', count: [] },
			{ label: '第八組', title: '第八組', count: [] }
	    ];

	    $scope.toggleCourse = function (item) {
	        $scope.students = [];
	        $scope.selectedCourse = item;

	        $scope.connection.send({
	            service: "GetClubMemberWithGroup",
	            body: { ClubID: item.id },
	            result: function (response, error, http) {
	                if (!error) {
	                    $scope.$apply(function () {
	                        $scope.students = [].concat(response.Student || []);
	                        $scope.students.forEach(function (item) {
	                            item.Phase = Math.abs(item.Phase);
	                            item.Group = Math.abs(item.Group) - 1;
	                        });
	                        $scope.countGroup();
	                    });
	                }
	            }
	        });
	    }

	    $scope.countGroup = function () {
	        $timeout(function () {
	            $scope.Groups.forEach(function (item) {
	                if ($scope.selectedCourse.fullPhase === 'false')
	                    item.count = [0, 0];
	                else
	                    item.count = [0];
	            });

	            $scope.students.forEach(function (item) {
	                if (item.Group >= 0 && $scope.Groups[item.Group]) {
	                    $scope.Groups[item.Group].count[item.Phase - 1] += 1;
	                }
	            });

	            $scope.Groups.forEach(function (item) {
	                //item.label = [item.title, ' (', item.count.join('、'), ')'].join('');
	                //item.label = item.title;
	                if (item.count[0] || item.count[1])
	                    item.memberCount = [' (', item.count.join('、'), ')'].join('');
	                else
	                    item.memberCount = '--';
	            });
	        });
	    }

	    $scope.submit = function () {
	        var data = [];
	        $scope.students.forEach(function (item) {
	            data.push({
	                StudentID: item.ID,
	                ClubID: $scope.selectedCourse.id,
	                Group: Math.abs(item.Group) + 1
	            });
	        });

	        $scope.connection.send({
	            service: "SaveStudentGroup",
	            body: {
	                Group: data
	            },
	            result: function (response, error, http) {
	                if (error)
	                    alert("儲存失敗");
	                else
	                    alert("儲存完成");
	            }
	        });
	    }
	}]);
