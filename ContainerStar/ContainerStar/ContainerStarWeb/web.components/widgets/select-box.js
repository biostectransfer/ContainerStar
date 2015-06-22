define([
	'lr!widgets/resources/select-box',
	'text!widgets/templates/select-box.html'
], function (Resources, Template) {   
	'use strict';

    $.widget('custom.selectBox', {
        options: {
            allowToSelectAll: true,
            selectAllItemsText: Resources.selectAllItemsText,
            placeholder: Resources.placeholder
        },

        values: function() {
            var values = [];
            this.element.find('option').each(function (index, elem) {
                if ($(elem).is(':selected'))
                    values.push(Number($(elem).val()));
            });

            return values;
        },

        destroy: function () {
            if (this._onBodyClick) {
                $('body').off('click', this._onBodyClick);
                this._onBodyClick = null;
            }

            if (this.$popupContainer) {
                this.$popupContainer.remove();
                this.$popupContainer = null;
            }
        },

        _create: function () {
            var self = this,
				template = self._template();

            self.options = _.defaults({
                placeholder: self.element.data('placeholder'),
                allowToSelectAll: self.element.data('allow-to-select-all'),
                selectAllItemsText: self.element.data('select-all-items-text')
            }, self.options);             
            
            self.$select = self.element.attr('multiple', 'multiple');
            self.$wrapperContainer = $(template).filter('span');
            self.$wrapper = self.$wrapperContainer.find('.k-dropdown-wrap');
            self.$popupContainer = $(template).filter('div');
            self.$popup = self.$popupContainer.find('.popup');
            self.$selectButton = self.$wrapper.find('.k-select');
            self.$ul = self.$popup.find('ul');
            self.$textBox = self.$wrapper.find('.k-input').attr('placeholder', self.options.placeholder);

            self.$select.after(self.$wrapperContainer).detach().appendTo(self.$wrapperContainer).hide();
            $(document.body).append(self.$popupContainer);

            if (_.isArray(self.options.dataSource)) {
                self.$select.empty();
                _.each(self.options.dataSource, function (item) {
                    self.$select.append('<option value="' + item[self.options.dataValueField] + '" >' + item[self.options.dataTextField] + '</option>');
                });
            }

            self._renderItems();

            if (self.options.allowToSelectAll)
                self._renderAllItem();

            self.restore();
            self._subscribe();			
        },

        _template: function() {
            var model = model = {
                id: this.element.attr('name'),
                okText: Resources.okText,
                cancelText: Resources.cancelText
            };

            return _.template(Template)(model);
        },

        _subscribe: function () {
            var self = this;

            this.$wrapper.hover(function (e) {
            	self.$wrapper.addClass('k-state-hover');
            }, function (e) {
            	self.$wrapper.removeClass('k-state-hover');
            });

            this.$ul.find('li').hover(function (e) {
                $(e.target).addClass('k-state-hover');
            }, function (e) {
                $(e.target).removeClass('k-state-hover');
            });

            this.$ul.delegate('li, :checkbox', 'click', function (e) {
            	e.stopPropagation();

            	var $target = $(e.target),
            		$checkBox = $target.is(':checkbox') ? $target : $target.find(':checkbox');

            	if ($checkBox !== $target)
            		$checkBox.prop('checked', !$checkBox.is(':checked'));

            	if($checkBox.is('.all'))
            		self.$checkBoxes.prop('checked', self.$checkBoxAll.is(':checked'));
            	else
            		self.$checkBoxAll.prop('checked', self.$checkBoxes.length == self.$checkBoxes.filter(':checked').length);
            });

            this.$popupContainer.mousedown(function (e) {
                e.stopPropagation();
                e.preventDefault();
            }).click(function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            this.$selectButton.mousedown(function (e) {
                e.stopPropagation();
                e.preventDefault();
            }).click(function (e) {
                e.stopPropagation();
                e.preventDefault();
            });

            this.$textBox.focus(function () {
                self.$wrapper.addClass('k-state-focused');
            }).blur(function () {
                self.$wrapper.removeClass('k-state-focused');
            });

            this.$selectButton.click(function (e) {
                e.stopPropagation();
                e.preventDefault();

                self._togglePopup();
                self._ensureFocus();
            });

            this.$popup.find('a:first').click(function () {
                self._save();
                self.$select.trigger('change');

                self._togglePopup(false);
                self._ensureFocus();
            });

            this.$popup.find('a:last').click(function () {
                self.restore();
                self._togglePopup(false);
                self._ensureFocus();
            });

            this._onBodyClick = function (e) {
                if (!$(e.srcElement || e.target).parents('#' + self.element.attr('name') + '_popup').length)
                    self._togglePopup(false);
            };

            $('body').on('click', this._onBodyClick);
        },

        _renderItems: function () {
            var self = this;

            self.$select.find('option').each(function (index, elem) {
                var $option = $(elem),
                    value = $option.val(),
                    text = $option.text(),
                    $li = $('<li class="k-item">' + text + '</li>'),
                    $checkBox = $('<input type="checkbox" value="' + value + '"/>').prependTo($li);

                self.$ul.append($li);
            });

            self.$checkBoxes = self.$ul.find(':checkbox');
           
        },

        _renderAllItem: function () {
            var $li = $('<li class="k-item">' + this.options.selectAllItemsText + '</li>').prependTo(this.$ul),
            $checkBox = $('<input class="all" type="checkbox" />').prependTo($li);

            this.$checkBoxAll = $checkBox;            
        },

        _save: function () {
            var self = this,
                values = [],
                text = [];

            self.$ul.find(':checked:not(.all)').each(function (index, elem) {
                values.push($(elem).val());
            });

            self.$select.find('option').each(function (index, elem) {
                var selected = _.contains(values, $(elem).val());
                $(elem).attr('selected', selected);

                if (selected)
                    text.push($(elem).text());
            });

            self._setText(text);
        },

        restore: function () {
            var self = this,
                values = [],
                text = [];

            self.$select.find('option:selected').each(function (index, elem) {
                values.push($(elem).val());
                text.push($(elem).text())
            });

            var $checkBoxes = self.$ul.find(':checkbox:not(.all)').each(function (index, elem) {
                $(elem).attr('checked', _.contains(values, $(elem).val()));
            });

            self.$ul.find('.all').prop('checked', $checkBoxes.length == $checkBoxes.filter(':checked').length);

            self._setText(text);
        },

        _setText: function(textItems) {
            var text = textItems.join('; '),
                title = textItems.join('[br] [checkbox] ');

            if (title)
                title = '[checkbox] ' + title;

            this.$textBox.val(text);
            this.$textBox.attr('title', title);
        },

        _ensureFocus: function () {
            if (this.$popupContainer.is(':visible'))
                this.$wrapper.removeClass('k-state-focused');
            else
                this.$textBox.focus();
        },

        _togglePopup: function (show) {
            this.$popupContainer.toggle(show);

            this._measurePopupSizeAndPosition();
        },

        _measurePopupSizeAndPosition: function () {
            var popupHeight = this.$popupContainer.outerHeight();
            if (popupHeight > 200) {
                popupHeight = 200;
                this.$popupContainer.find('ul').css({
                    'height': '200px',
                    'overflow-y': 'scroll'
                });
            }

            var offsetTop = this.$wrapperContainer.offset().top,
                windowHeight = $(window).outerHeight(),
                windowWidth = $(window).outerWidth(),
                pageYOffset = window.pageYOffset,
                textBoxWidth = this.$wrapperContainer.outerWidth(),
                textBoxLeftOffset = this.$wrapperContainer.offset().left;

            this.$popup.removeClass('border-up border-down');

            if ((offsetTop - pageYOffset) + popupHeight > windowHeight) {
                this.$popupContainer.css('top', offsetTop - this.$popup.outerHeight() - 1);
                this.$popup.addClass('border-down');
            }
            else {
                this.$popupContainer.css('top', offsetTop + this.$wrapperContainer.outerHeight() + 1);
                this.$popup.addClass('border-up');
            }

            if (textBoxWidth < 190)
                textBoxWidth = 190;

            if (textBoxLeftOffset + textBoxWidth > windowWidth)
                textBoxLeftOffset -= textBoxLeftOffset + textBoxWidth - windowWidth;

            this.$popupContainer.css('left', textBoxLeftOffset);
            this.$popupContainer.css('width', textBoxWidth + 'px');
        }
	});
});