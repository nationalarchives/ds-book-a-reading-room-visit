<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <h3 style="margin-top: 2em;">Booking summary </h3>
      <table style="text-align: left;border: 1px solid #999;border-collapse: collapse;margin: 2em 0;">
        <tbody>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Name</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
				<span style="color: #d4351c;font-weight: bold;">
					<xsl:value-of select="Name" />
				</span>
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Telephone</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="Phone" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Reader’s ticket number</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="ReaderTicket" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Booking reference</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="BookingReference" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit date</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Seat number</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
				<span style="color: #d4351c;font-weight: bold;">
					<xsl:value-of select="SeatNumber" />
				</span>
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Access needs and research requests</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="AdditionalRequirements" />
            </td>
          </tr>
        </tbody>
      </table>

      <hr />
      <h3 style="margin-top: 2em;">Document order</h3>

      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
			<xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
			<xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>
    </body>
  </xsl:template>
</xsl:stylesheet>
