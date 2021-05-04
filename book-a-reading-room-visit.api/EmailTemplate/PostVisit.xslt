﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        Thank you for booking a visit to The National Archives.
      </p>
      <p>
        If you attended, we would love to know how your visit went by completing our short survey. Whether this was your first visit, or one of many, we really value the opinions of our visitors. All feedback will be taken into consideration as we evaluate the current measures we have introduced to ensure the safety of our visitors and staff.
      </p>
      <p>
        The survey will take just a few minutes to complete. To take part, please 
        <a href="https://www.smartsurvey.co.uk/s/TGSXIJ/">click here</a>.
      </p>
      <p>
        This email inbox is not being monitored.
      </p>
    </body>
  </xsl:template>
</xsl:stylesheet>