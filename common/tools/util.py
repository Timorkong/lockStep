# !/usr/bin/python
# encoding=utf-8
# version: 2018-04-11 23:53:19

import os

class Util(object):
    """
    Util
    """
    def __init__(self):
        pass
    
    @staticmethod
    def strip_notes_spaces_tab(text):
        """
        移除注释/tab转空格/多个空格变一个
        """
        if text.find('//') != -1:
            text = text.split('//', 1)[0]
        text = text.replace('\t', ' ')
        while text.find('  ') != -1:
            text = text.replace('  ', ' ')
        return text.strip()
    
    @staticmethod
    def empty_str(text):
        """
        字符串为空
        """
        if len(text) <= 0:
            return True
        return False
