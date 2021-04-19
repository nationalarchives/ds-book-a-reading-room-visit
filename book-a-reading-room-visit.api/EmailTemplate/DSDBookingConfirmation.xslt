<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <style>
      ul {
      padding: 0;
      margin: 30px 0;
      }
      ul li {
      list-style-type: none;
      margin-bottom: 10px;
      padding-bottom: 10px;
      }
    </style>
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <h3 style="margin-top: 2em;">Booking summary </h3>

      <ul>
        <li>
          Name: <b>
            <xsl:value-of select="Name" />
          </b>
        </li>
        <li>
          Telephone: <xsl:value-of select="Phone" />
        </li>
        <li>
          Reader's ticket number: <xsl:value-of select="ReaderTicket" />
        </li>
        <li>
          Booking reference: <xsl:value-of select="BookingReference" />
        </li>
        <li>
          Date of visit: <xsl:value-of select="VisitStartDate" />
        </li>
        <li>
          Seat: <b>
            <xsl:value-of select="SeatNumber" />
          </b>
        </li>
        <li>
          Face covering: <xsl:value-of select="FaceCovering" />
        </li>
        <li>
          Access needs and research requests: <xsl:value-of select="AdditionalRequirements" />
        </li>
      </ul>
      <hr />
      <h3 style="margin-top: 2em;">Document order</h3>

      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" />
        </p>
      </xsl:for-each>
    </body>
  </xsl:template>
</xsl:stylesheet>
