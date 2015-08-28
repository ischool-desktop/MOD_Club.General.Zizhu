if (!('forEach' in Array.prototype)) {
    Array.prototype.forEach= function(action, that /*opt*/) {
        for (var i= 0, n= this.length; i<n; i++)
            if (i in this)
                action.call(that, this[i], i, this);
    };
}

angular.module('MyApp', []).controller('MyController', ['$scope', '$timeout', function ($scope, $timeout) {
	$scope.current = {
		schoolYear: "2015",
		semester: "1",
		levelMax: 2,
		grade: "四年级",
		//mode: "select",
		mode: "view",
		shown: null,
		progress: 0,
		selected: [null, null, null]
	};
	$scope.srcList = [
		"http://pic1.ooopic.com/uploadfilepic/yuanwenjian/2009-10-17/OOOPIC_760996499_20091017bc8dfbd1a1a24095.jpg",
		"http://upaiyun.gogo.com.cn//Uploads/20141222/1419230745_33129.jpg!super",
		"http://img9.3lian.com/c1/vector/20/01/13.jpg",		
		"http://www.itingwa.com/file/img/2014-02/20140203102351-NTMxODQy_500x361.jpg",
		"http://pic.shejiben.com/wzcourse/94/28f952df26cc50fc11828e2aeb76bd6f.jpg",
		"http://img.taopic.com/uploads/allimg/110717/1616-110GGG52875.jpg",
		"http://www.chinahexie.org.cn/uploads/allimg/110525/8_110525134028_1.jpg"
	];
	$scope.courseFull = {
		"1": [0, 0, 0],
		"2": [0, 0, 0],
		"3": [0, 0, 0],
		"4": [0, 0, 0],
		"5": [0, 0, 0],
		"6": [0, 0, 0],
		"7": [0, 0, 0],
		"8": [0, 0, 0],
		"9": [0, 0, 0],
		"10": [0, 0, 0],
		"11": [0, 0, 0],
		"12": [0, 0, 0],
		"13": [0, 0, 0],
		"14": [0, 0, 0],
		"15": [0, 0, 0],
		"16": [0, 0, 0],
		"17": [0, 0, 0]
	};
	var courseSpeed = {
		"1": [0, 9, 0],
		"2": [0, 2, 0],
		"3": [0.8, 6, 0],
		"4": [0, 0, 0],
		"5": [0, 0, 0],
		"6": [0, 12, 0],
		"7": [0, 0, 0],
		"8": [14, 0, 0],
		"9": [0, 0, 0],
		"10": [13, 0, 0],
		"11": [10, 0, 0],
		"12": [0, 0, 0],
		"13": [0, 0, 0],
		"14": [0, 0, 0],
		"15": [14, 12, 0],
		"16": [3, 4, 0],
		"17": [0, 0, 0]
	}
	var courseLimit = {
		"1": [45, 50, 0],
		"2": [49, 50, 0],
		"3": [50, 50, 0],
		"4": [49, 32, 0],
		"5": [0, 0, 0],
		"6": [0, 50, 0],
		"7": [0, 0, 0],
		"8": [50, 33, 0],
		"9": [0, 0, 0],
		"10": [50, 0, 0],
		"11": [50, 0, 0],
		"12": [32, 49, 0],
		"13": [0, 49, 0],
		"14": [0, 49, 0],
		"15": [50, 50, 0],
		"16": [50, 50, 0],
		"17": [0, 0, 0]
	};
	var realFull = {
		"1": [45, 50, 0],
		"2": [49, 50, 0],
		"3": [50, 50, 0],
		"4": [49, 32, 0],
		"5": [0, 0, 0],
		"6": [0, 50, 0],
		"7": [0, 0, 0],
		"8": [50, 33, 0],
		"9": [0, 0, 0],
		"10": [50, 0, 0],
		"11": [50, 0, 0],
		"12": [32, 50, 0],
		"13": [0, 50, 0],
		"14": [0, 50, 0],
		"15": [49, 49, 0],
		"16": [49, 49, 0],
		"17": [0, 0, 0]
	};

	$scope.courseList = [
		{
			id: 1,
			classification: '科技与创新',
			name: '指尖上的舞蹈',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 2,
			classification: "运动与生命",
			name: '趣味武术',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 3,
			classification: "运动与生命",
			name: '学弈',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 4,
			classification: "运动与生命",
			name: '舞向未来',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 5,
			classification: "运动与生命",
			name: '旋风跆拳道',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 6,
			classification: "运动与生命",
			name: '“舞”与“轮”比',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 7,
			classification: "社会与生活",
			name: '小小营养师',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 8,
			classification: "世界与未来",
			name: '书法欣赏',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 9,
			classification: "世界与未来",
			name: '儿童哲学',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 10,
			classification: "世界与未来",
			name: '趣味历史',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 11,
			classification: "文学与艺术",
			name: '奇思妙想创意画',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 12,
			classification: "文学与艺术",
			name: '儿童剧社——欢乐剧场',
			limit: {
				"一年级": { all: 50, male: 25, female: 25 },
				"二年级": { all: 50, male: 25, female: 25 },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: null, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 13,
			classification: "文学与艺术",
			name: '儿童装饰画',
			limit: {
				"一年级": { all: 50, male: 25, female: 25 },
				"二年级": { all: 50, male: 25, female: 25 },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: null, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 14,
			classification: "文学与艺术",
			name: '创意美术魔法师',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: 25, female: 25 },
				"五年级": { all: 50, male: 25, female: 25 }
			}
		},
		{
			id: 15,
			classification: "文学与艺术",
			name: 'U玩漫画',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 16,
			classification: "文学与艺术",
			name: '趣味素描',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: 50, male: null, female: null },
				"五年级": { all: null, male: null, female: null }
			}
		},
		{
			id: 17,
			classification: "文学与艺术",
			name: '毕业剧',
			limit: {
				"一年级": { all: null, male: null, female: null },
				"二年级": { all: null, male: null, female: null },
				"三年级": { all: null, male: null, female: null },
				"四年级": { all: null, male: null, female: null },
				"五年级": { all: 50, male: 25, female: 25 }
			}
		}
	];

	$scope.reflash = function () {
		var courseWithClassification = {};
		var resetShown = true;
		var first = null;
		$scope.courseList.forEach(function (course) {
			if (($scope.current.mode == "select" && course.limit[$scope.current.grade].all) ||
				($scope.current.mode == "view")
				) {
				if (!courseWithClassification[course.classification])
					courseWithClassification[course.classification] = [];
				courseWithClassification[course.classification].push(course);


				if ($scope.current.shown == course) {
					resetShown = false;
				}
				if (first == null)
					first = course;
			}
		});


		if (resetShown)
			$scope.current.shown = first;
		$scope.courseWithClassification = courseWithClassification;

	}
	$scope.selectCourse = function (course, level) {
		if ($scope.current.selected[level]) {
			if ($scope.current.selected[level] == course) {
				$scope.current.selected[level] = null;
				$scope.courseFull[course.id][level]--;
			}
		}
		else {
			if (realFull[course.id][level] >= course.limit[$scope.current.grade].all){
				course.showFull = true;
				if (course.showFullTimer) {
					$timeout.cancel(course.showFullTimer);
				}
				course.showFullTimer= $timeout(function () {
					delete course.showFull;
					delete course.showFullTimer;
				}, 2000);
			}
			else {
				$scope.current.selected[level] = course;
				$scope.courseFull[course.id][level]++;
			}
		}

		var scount = 0;
		for (var i = 0; i < $scope.current.levelMax; i++) {
			if ($scope.current.selected[i])
				scount++;
		}
		switch ($scope.current.levelMax) {
			case 1:
				if (scount == 0)
					$scope.current.progress = 0;
				if (scount == 1)
					$scope.current.progress = 100;
				break;
			case 2:
				if (scount == 0)
					$scope.current.progress = 0;
				if (scount == 1)
					$scope.current.progress = 50;
				if (scount == 2)
					$scope.current.progress = 100;
				break;
			case 3:
				if (scount == 0)
					$scope.current.progress = 0;
				if (scount == 1)
					$scope.current.progress = 33;
				if (scount == 2)
					$scope.current.progress = 66;
				if (scount == 3)
					$scope.current.progress = 100;
				break;
		}
	}
	$scope.selectDisable = function (course, level) {
		for (var i = 0; i < $scope.current.levelMax; i++) {
			if (i == level) {
				if ($scope.current.selected[i] && $scope.current.selected[i] != course)
					return true;
			}
			else {
				if ($scope.current.selected[i] && $scope.current.selected[i].classification == course.classification) {
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
		$scope.current.student = { name: '王小民' };

	}
	$scope.logout = function () {
		delete $scope.current.student;
		$scope.current.mode = 'view';
	}
	var countTime = function () {

		function getTimeString(timespan) {
			function pad(num, size) {
				var s = "000000000" + num;
				return s.substr(s.length - size);
			}
			if (timespan < 60) {//一分內
				return "00:00:" + pad(timespan, 2);
			}
			else if (timespan < 3600) {//一小時內
				return "00:" + pad(parseInt(timespan / 60), 2) + ":" + pad(timespan % 60, 2);
			}
			else if (timespan < 86400) {//一天內
				return pad(parseInt(timespan / 3600), 2) + ":" + pad(parseInt((timespan % 3600) / 60), 2) + ":" + pad(timespan % 60, 2);
			}
			else {
				return parseInt(timespan / 86400) + "天"
			}
		}

		var now = new Date().getTime();
		if (now >= $scope.timing.end)
			$scope.timing.msg = "已经结束";
		else {
			if (now >= $scope.timing.start) {
				$scope.timing.msg = "将于" + getTimeString(parseInt(($scope.timing.end - now) / 1000)) + "後結束";

				if ($scope.current.student) {
					if ($scope.current.mode != 'select') {
						$scope.current.mode = 'select';
						$scope.reflash();
					}

					for (var i = 1 ; i <= 17; i++) {
						for (var j = 0; j < 3; j++) {
							if ($scope.courseFull[i][j] < courseLimit[i][j]) {
								$scope.courseFull[i][j] += courseSpeed[i][j];
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
			}
			else {
				$scope.timing.msg = "将于" + getTimeString(parseInt(($scope.timing.start - now) / 1000)) + "後開始";
				if ($scope.current.mode != 'view') {
					$scope.current.mode = 'view';
					$scope.reflash();
				}
			}
			countTimer = $timeout(countTime, 1000);
		}
	}
	$scope.setTrial = function () {
		var now = new Date().getTime();
		$scope.timing = {
			start: now + 20000,
			end: now + 200000
		};
		$scope.current.mode = 'view';
		countTime();
	}
	$scope.setTrial();
	$scope.reflash();
	var countTimer = $timeout(countTime, 1000);
}
]);